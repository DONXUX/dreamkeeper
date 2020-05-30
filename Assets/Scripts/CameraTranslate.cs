using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslate : MonoBehaviour
{
	[SerializeField] public float speed = 5.0f;
	[SerializeField] public float gravity = -9.8f;
	[SerializeField] public float jumpPower = 3.0f;
	[SerializeField] public int maxJumpCount = 2;
	int jumpCount = 0;
	float yVelocity;

	CharacterController cc;
	void Start()
	{
		cc = gameObject.GetComponent<CharacterController>();
	}

	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		/// 이동은 Y축 제외하기
		Vector3 dir = new Vector3(h, 0, v);
		dir.Normalize();

		/// 내가 바라보는 방향으로 이동
		dir = Camera.main.transform.TransformDirection(dir);

		/// 바닥에 닿으면 점프 정보 초기화
		if(cc.collisionFlags == CollisionFlags.Below)
		{
			jumpCount = 0;
			yVelocity = 0;
		}
		
		/// 점프
		if (Input.GetKey(KeyCode.Space))
		{
			if(jumpCount == 0 && cc.collisionFlags != CollisionFlags.Below)
			{
				return;
			}
			else if(jumpCount < maxJumpCount)
			{
				yVelocity = jumpPower;
				jumpCount++;
			}
		}

		/// 대쉬
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			speed *= 2;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed /= 2;
		}

		yVelocity += gravity * Time.deltaTime;
		dir.y = yVelocity;

		cc.Move(dir * speed * Time.deltaTime);
	}
}
