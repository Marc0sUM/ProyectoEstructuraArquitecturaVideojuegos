using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int enemiesAlive = 0;
        private bool _spawnedEmerald;
         [Header("Spawn de esmeralda")]
    [SerializeField] private GameObject emeraldPrefab;
    [SerializeField] private float spawnHeight = 12f;       // qué tan alto spawn
    [SerializeField] private float extraDownForce = 4f;     // empujón extra hacia abajo
    [SerializeField] private Transform fallbackSpawnPoint;

     private int _alive;

    void Start()
    {
        Debug.Log("EnemyManager ready");
    }

    public void AddEnemy()
    {
        enemiesAlive++;
        Debug.Log("Enemy added. Total alive: " + enemiesAlive);
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;
        Debug.Log("Enemy defeated! Remaining: " + enemiesAlive);

        if (enemiesAlive <= 0)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        Debug.Log("VICTORY! Level Complete!");
         _alive = Mathf.Max(0, _alive - 1);
        if (_alive == 0 && !_spawnedEmerald)
        {
            _spawnedEmerald = true;
            SpawnEmeraldAbovePlayer();
        }
        //FindObjectOfType<MainMenuManager>().ShowGameOverMenu();
    }

      private void SpawnEmeraldAbovePlayer()
    {
        if (emeraldPrefab == null)
        {
            Debug.LogError("[EnemyManager] emeraldPrefab no asignado.");
            return;
        }

        Transform player = FindPlayerTransform();
        Vector3 basePos = player != null ? player.position : (fallbackSpawnPoint ? fallbackSpawnPoint.position : Vector3.zero);
        Vector3 spawnPos = basePos + Vector3.up * spawnHeight;

        GameObject emerald = Instantiate(emeraldPrefab, spawnPos, Quaternion.identity);

        // Asegura caída “bonita”
        if (emerald.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.down * extraDownForce, ForceMode.VelocityChange);
        }
        if (emerald.TryGetComponent<Animator>(out var anim)) anim.SetTrigger("Fall");
    }

      private Transform FindPlayerTransform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player ? player.transform : null;
    }
}