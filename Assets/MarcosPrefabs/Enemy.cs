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
}