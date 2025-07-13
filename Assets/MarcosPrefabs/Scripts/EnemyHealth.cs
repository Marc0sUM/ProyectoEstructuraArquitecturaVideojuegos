using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    private Rigidbody2D rb;

    public float knockbackForce = 5f;

    private EnemyManager enemyManager; // 👈 Referencia al manager

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.AddEnemy();
        }
        else
        {
            Debug.LogWarning("EnemyManager no encontrado en la escena");
        }
    }

    public void TakeDamage(int damage, Vector2 knockbackDir)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        if (rb != null)
        {
            rb.velocity = Vector2.zero; 
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

        if (enemyManager != null)
        {
            enemyManager.EnemyDefeated();
        }

        Destroy(gameObject);
    }
}