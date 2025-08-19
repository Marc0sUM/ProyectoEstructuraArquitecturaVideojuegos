using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldPickup : MonoBehaviour
{
    [SerializeField] private float autoDestroySeconds = 30f;

    private void Start()
    {
        if (autoDestroySeconds > 0f)
            Destroy(gameObject, autoDestroySeconds);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Cuando el jugador la recoge, mostramos menú de victoria
        FindObjectOfType<MainMenuManager>().ShowGameOverMenu();

        Destroy(gameObject);
    }
}