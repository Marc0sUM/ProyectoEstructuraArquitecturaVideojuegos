using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    private Rigidbody2D rb;

    public float knockbackForce = 5f;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 knockbackDir)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Opcional: limpia velocidad anterior
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
