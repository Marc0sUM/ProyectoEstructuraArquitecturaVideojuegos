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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        var menu = FindObjectOfType<LvlManagerC>();
        if (menu != null) menu.ShowGameLevelOverMenu();

        Destroy(gameObject);
    }
}