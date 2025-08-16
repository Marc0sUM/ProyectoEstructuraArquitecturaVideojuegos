using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldPickup : MonoBehaviour
{
    [SerializeField] private float autoDestroySeconds = 30f;

    private void Start()
    {
        if (autoDestroySeconds > 0f) Destroy(gameObject, autoDestroySeconds);
    }

    private void OnTriggerEnter(Collider other) // usa OnTriggerEnter2D si es 2D
    {
        if (!other.CompareTag("Player")) return;

        FindObjectOfType<MainMenuManager>().ShowGameOverMenu();

        Destroy(gameObject);
    }
}
