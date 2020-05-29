using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 속력
    [SerializeField] float speed = 20f;
    [SerializeField] ParticleSystem hitParticlePrefab;
    void Start()
    {
        // 속도 (전방향)
        var velocity = speed * transform.forward;

        var rigidbody = GetComponent<Rigidbody>();
        // 지정한 속도변화 만큼의 힘을 가하는 함수
        rigidbody.AddForce(velocity, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 콜라이더 끼리 닿을 때 호출
        other.SendMessage("OnHitBullet");
        Instantiate(hitParticlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}