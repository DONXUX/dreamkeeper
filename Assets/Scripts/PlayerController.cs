using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	public float speed = 5.0f;
	public float gravity = -9.8f;
	public float jumpForce = 3.0f;
	public int maxJumpCount = 2;

	[Header("Rotation")]
	// 회전속도
	public float angularVelocity = 30f;

	// 수평 및 수직 방향의 회전량(각도)의 매 프레임 누적 값
	float horizontalAngle = 0f;
	float verticalAngle = 0f;

	private int jumpCount = 0;
	private float yVelocity;

	private float h, v;
	private Vector3 dir;

	private Crosshair theCrosshair;

	// 상태 변수
	private bool isWalk = false;
	private bool isRun = false;
	private bool isCrouch = false;
	private bool isGround = true;

	// 움직임 체크 변수
	private Vector3 lastPos;

	CharacterController cc;
	void Start()
	{
		cc = gameObject.GetComponent<CharacterController>();
		theCrosshair = FindObjectOfType<Crosshair>();
	}

	void Update()
	{
		Init();
		OnGround();
		Jump();
		Run();
		Rotate();
		MoveCheck();
		Move();
	}

	private void Init()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
	}

	private void OnGround()
	{
		/// 바닥에 닿으면 점프 정보 초기화
		if (cc.collisionFlags == CollisionFlags.Below)
		{
			jumpCount = 0;
			yVelocity = 0;
		}
	}

	private void Jump()
	{
		/// 점프
		if (Input.GetKey(KeyCode.Space))
		{
			if (jumpCount == 0 && cc.collisionFlags != CollisionFlags.Below)
			{
				return;
			}
			else if (jumpCount < maxJumpCount)
			{
				yVelocity = jumpForce;
				jumpCount++;
			}
		}
	}

	private void Run()
	{
		/// 대쉬
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			speed *= 2;
			isRun = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed /= 2;
			isRun = false;
		}
		theCrosshair.RuningAnimation(isRun);
	}

	private void Move()
	{
		/// 이동은 Y축 제외하기
		dir = new Vector3(h, 0, v);
		dir.Normalize();

		/// 내가 바라보는 방향으로 이동
		dir = Camera.main.transform.TransformDirection(dir);

		yVelocity += gravity * Time.deltaTime;
		dir.y = yVelocity;

		cc.Move(dir * speed * Time.deltaTime);
	}

	private void MoveCheck()
	{
		if (!isRun)
		{
			if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
				isWalk = true;
			else
				isWalk = false;
			theCrosshair.WalkingAnimation(isWalk);
			lastPos = transform.position;
		}
	}

	private void Rotate()
    {
		// 마우스 커서 비가시, 고정 모드
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		// 전 프레임에서 현 프레임까지의 수평/수직 회전량(각도)
		var horizontalRotation = Input.GetAxis("Mouse X") * angularVelocity * Time.deltaTime;
		var verticalRotation = -Input.GetAxis("Mouse Y") * angularVelocity * Time.deltaTime;

		horizontalAngle += horizontalRotation;
		verticalAngle += verticalRotation;

		// 누적 각도의 최소/최대값 제한
		verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

		// 오일러 방식에 의한 회전(zxy순)을 쿼터니언 방식의 회전으로 변환
		transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0f);
	}
}