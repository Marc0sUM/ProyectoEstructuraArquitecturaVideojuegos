using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesClearedBlock : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;     // en EmeraldM
    [SerializeField] private bool hideOnStart = true;
    [SerializeField] private bool stayVisibleOnceCleared = true;

    private EnemyManager manager;

    private void Awake()
    {
        if (group == null) group = GetComponent<CanvasGroup>();
        if (group == null) group = gameObject.AddComponent<CanvasGroup>();

        if (hideOnStart) Hide();
    }

    private void OnEnable()
    {
        manager = FindObjectOfType<EnemyManager>();
        if (manager != null) manager.OnRemainingChanged += HandleRemainingChanged;
    }

    private void OnDisable()
    {
        if (manager != null) manager.OnRemainingChanged -= HandleRemainingChanged;
    }

    private void HandleRemainingChanged(int remaining)
    {
        if (remaining <= 0) Show();
        else if (!stayVisibleOnceCleared) Hide();
    }

    private void Show()
    {
        group.alpha = 1f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    private void Hide()
    {
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
