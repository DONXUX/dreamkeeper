using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shooter : MonoBehaviour
{
    [SerializeField] Transform gunBarrelEnd;
    [SerializeField] ParticleSystem gunParticle;
    [SerializeField] AudioSource gunAudioSource;
    [SerializeField] float maxDistance;
    private Crosshair theCrosshair;

    float timer = 0.0f;
    int waitingTime = 2;

    RaycastHit hit;

    private void Start()
    {
        theCrosshair = FindObjectOfType<Crosshair>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            timer += Time.deltaTime;
            Shoot();
            if (timer > waitingTime)
            {
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        theCrosshair.FireAnimation();
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.blue, 0.3f);

        // 레이 캐스트
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            if(hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.SendMessage("OnHitBullet");
            } 
        }
        // 파티클 
        gunParticle.Play();
        // 총 효과음 재생
        gunAudioSource.Play();
    }
}
