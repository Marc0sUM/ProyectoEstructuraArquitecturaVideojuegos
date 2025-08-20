using UnityEngine;
using UnityEngine.UI;

public class EnemiesRemainingView : MonoBehaviour
{
    [SerializeField] private Text label;       
    [SerializeField] private string format = "Enemigos restantes: {0}";
    [SerializeField] private bool hideWhenZero = false;

    private EnemyManager manager;

    private void Awake()
    {
        if (label == null) label = GetComponent<Text>();
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
        if (label != null)
            label.text = string.Format(format, remaining);

        if (hideWhenZero && label != null)
            label.gameObject.SetActive(remaining > 0);
    }
}
