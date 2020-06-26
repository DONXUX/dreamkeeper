using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerShooting : MonoBehaviour
{
	[Header("Weapon")]
	public ParticleSystem gunParticle;
	public ParticleSystem hitParticle;
	public AudioSource gunAudioSource;
	public float maxRange;

	float timer = 0.0f;
	int waitingTime = 2;

	LineRenderer gunLine;
	Light gunLight;
	float effectDisplayTime = 0.2f;
	float timeBetweenBullets = 0.1f;

	Ray shootRay;
	RaycastHit shootHit;

	private Crosshair theCrosshair;

	void Start()
    {
		gunParticle = GetComponent<ParticleSystem>();
		theCrosshair = FindObjectOfType<Crosshair>();
		gunLine = GetComponent<LineRenderer>();
		gunLight = GetComponent<Light>();
	}

    // Update is called once per frame
    void Update()
    {
		Shoot();
    }

	private void Shoot()
	{
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
		{
			timer = 0f;
			theCrosshair.FireAnimation();
			Debug.DrawRay(transform.position, transform.forward * maxRange, Color.red, 0.3f);

			gunLight.enabled = true;

			gunLine.enabled = true;
			gunLine.SetPosition(0, transform.position);

			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

			// 레이 캐스트
			if (Physics.Raycast(shootRay, out shootHit, maxRange))
			{
				Collider collider = shootHit.collider;
				if (collider.tag == "Enemy")
				{
					collider.gameObject.SendMessage("OnHitBullet");
					Instantiate(hitParticle, shootHit.point, Quaternion.identity);
				}
				gunLine.SetPosition(1, shootHit.point);
			}
			else
			{
				gunLine.SetPosition(1, shootRay.origin + shootRay.direction * maxRange);
			}
			// 파티클 
			gunParticle.Stop();
			gunParticle.Play();
			// 총 효과음 재생
			gunAudioSource.Play();
			if (timer > waitingTime)
			{
				timer = 0;
			}
		}

		if (timer >= timeBetweenBullets * effectDisplayTime)
		{
			DisableEffects();
		}
	}

	void DisableEffects()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}
}
