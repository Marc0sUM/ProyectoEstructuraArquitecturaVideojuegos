using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;      // Asigna tu personaje en el Inspector
    public float speed = 2f;      // Velocidad de movimiento
    public float chaseDistance = 5f; // Distancia de detección

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            // Moverse hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Collision with: " + collision.gameObject.name);

    if (collision.gameObject.CompareTag("Player"))
    {
        Debug.Log("Player hit!");
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(20);
        }
    }
}
}