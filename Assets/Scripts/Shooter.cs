using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunBarrelEnd;
    [SerializeField] ParticleSystem gunParticle;
    [SerializeField] AudioSource gunAudioSource;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 총알 복제 : 복제 대상, 초기위치, 초기방향
        Instantiate(bulletPrefab, gunBarrelEnd.position, gunBarrelEnd.rotation);
        // 파티클 
        gunParticle.Play();
        // 총 효과음 재생
        gunAudioSource.Play();
    }
}
