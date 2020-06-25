using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public Slider healthBar;
    public float flashSpeed = 3.0f;
    public Color flashColor;
    public Image damageImage;
    bool damage;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
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
}
