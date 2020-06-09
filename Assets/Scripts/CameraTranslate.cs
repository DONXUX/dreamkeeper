using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraTranslate : MonoBehaviour
{
	[SerializeField]
	public float speed = 5.0f;
	public float gravity = -9.8f;
	public float jumpPower = 3.0f;
	public int maxJumpCount = 2;
	private Crosshair theCrosshair;

	private int jumpCount = 0;
	private float yVelocity;

	private float h, v;
	private Vector3 dir;

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
				yVelocity = jumpPower;
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
}