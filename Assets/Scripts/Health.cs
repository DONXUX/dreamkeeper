using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public Slider healthBar;
    public float flashSpeed = 3.0f;
    public Color flashColor;
    public Image damageImage;
    public Image gameOverImage;
    public Animator animator;
    bool damage;
    private float health;
    private float fades = 0.0f;
    private float timer = 0;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (damage)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damage = false;

        if (health <= 0)
        {
            Death();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            damage = true;
            health -= collision.gameObject.GetComponent<Enemy>().attackPower;
            healthBar.value = health / 100.0f;
            Debug.Log(health);
        }
    }

    private void Death()
    {
        animator.SetTrigger("Die");
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponentInChildren<PlayerShooting>().enabled = false;

        timer += Time.deltaTime;
        if(fades < 1.0f && timer >= 0.1f)
        {
            fades += 0.01f;
            print(fades);
            gameOverImage.color = new Color(0, 0, 0, fades);
        }
        else if (fades >= 1.0f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
