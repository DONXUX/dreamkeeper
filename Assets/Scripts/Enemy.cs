using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public AudioClip spawnClip;
    public AudioClip hitClip;

    public Collider enemyCollider;
    public Renderer enemyRenderer;

    public Animator animator;
    public int maxHealth;
    public float attackPower;
    public float sinkSpeed = 2.5f;

    private int health;
    NavMeshAgent nav;
    GameObject target;
    CapsuleCollider capsuleCollider;

    AudioSource audioSource;

    bool isSinking;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnClip);
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        health = maxHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        else
        {
            Follow();
        }
    }
    void Follow()
    {
        if(nav.destination != target.transform.position)
        {
            nav.SetDestination(target.transform.position);
        }
        else
        {
            nav.SetDestination(transform.position);
        }
    }
    void OnHitBullet()
    {        
        audioSource.PlayOneShot(hitClip);

        health--;
        Debug.Log(health);

        if (health <= 0)
        {
            Debug.Log("죽음");
            Death();
        }
    }

    void Death()
    {
        capsuleCollider.isTrigger = true;
        animator.SetTrigger("Dead");
        Destroy(gameObject, 5.0f);
    }
    
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
}
