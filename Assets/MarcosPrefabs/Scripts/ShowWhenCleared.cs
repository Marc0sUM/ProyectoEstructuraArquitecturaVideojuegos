using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWhenCleared : MonoBehaviour
{
    [SerializeField] private Text label;     
    [SerializeField] private string message = "¡Todos los enemigos derrotados!";

    private EnemyManager manager;

    private void Awake()
    {
        // Ocultamos SOLO el texto al inicio, no el GameObject padre
        if (label != null)
            label.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        manager = FindObjectOfType<EnemyManager>();
        if (manager != null)
            manager.OnRemainingChanged += HandleRemainingChanged;
    }

    private void OnDisable()
    {
        if (manager != null)
            manager.OnRemainingChanged -= HandleRemainingChanged;
    }

    private void HandleRemainingChanged(int remaining)
    {
        if (label == null) return;

        if (remaining <= 0)
        {
            label.text = message;
            label.gameObject.SetActive(true);
        }
        else
        {
            label.gameObject.SetActive(false);
        }
    }
}