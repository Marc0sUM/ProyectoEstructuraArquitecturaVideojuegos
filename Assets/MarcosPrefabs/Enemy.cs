using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EthanTheHero;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAnimation playerAnim = collision.gameObject.GetComponent<PlayerAnimation>();
            if (playerAnim != null)
            {
                playerAnim.TakeDamage();
            }
        }
    }

    public void TakeDamage()
    {
        Debug.Log("¡El enemigo ha sido eliminado!");
        Destroy(gameObject); // Desaparece el enemigo
    }

}