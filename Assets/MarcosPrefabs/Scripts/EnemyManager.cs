using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemigos")]
    private List<Enemy> enemies = new List<Enemy>();
    private bool _spawnedEmerald;

    [Header("Spawn de esmeralda")]
    [SerializeField] private GameObject emeraldPrefab;
    [SerializeField] private float spawnHeight = 12f;
    [SerializeField] private float extraDownForce = 4f;
    [SerializeField] private Transform fallbackSpawnPoint;

    void Start()
    {
        Debug.Log("EnemyManager listo");
    }

    private void Update()
    {
        // Si ya no quedan enemigos vivos
        if (enemies.Count <= 0 && !_spawnedEmerald)
        {
            LevelComplete();
        }
    }

    // ==== Métodos llamados desde Enemy o EnemyHealth ====

    public void AddEnemy(Enemy enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
            Debug.Log("Enemigo agregado. Total: " + enemies.Count);
        }
    }

    public void EnemyDefeated(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Debug.Log("Enemigo derrotado. Restantes: " + enemies.Count);
        }

        if (enemies.Count <= 0)
        {
            LevelComplete();
        }
    }

    // ==== Spawning de la esmeralda ====

    private void LevelComplete()
    {
        Debug.Log("VICTORIA! Nivel completado.");

        if (!_spawnedEmerald)
        {
            _spawnedEmerald = true;
            SpawnEmeraldAbovePlayer();
        }
    }

    private void SpawnEmeraldAbovePlayer()
    {
        if (emeraldPrefab == null)
        {
            Debug.LogError("[EnemyManager] emeraldPrefab no asignado.");
            return;
        }

        Transform player = FindPlayerTransform();
        Vector3 basePos = player != null ? player.position :
                          (fallbackSpawnPoint ? fallbackSpawnPoint.position : Vector3.zero);

        Vector3 spawnPos = basePos + Vector3.up * spawnHeight;

        GameObject emerald = Instantiate(emeraldPrefab, spawnPos, Quaternion.identity);

        // Asegurar caída con físicas
        if (emerald.TryGetComponent<Rigidbody2D>(out var rb2d))
        {
            rb2d.gravityScale = 1f; // Activa gravedad
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
            rb2d.AddForce(Vector2.down * extraDownForce, ForceMode2D.Impulse);
        }

        if (emerald.TryGetComponent<Animator>(out var anim))
            anim.SetTrigger("Fall");
    }

    private Transform FindPlayerTransform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player ? player.transform : null;
    }
}




/*

public class EnemyManager : MonoBehaviour
{

    
    public List<GameObject> enemies = new List<GameObject>();

    public GameObject emeraldPrefab; // Prefab de la gema
    public Transform spawnPoint;     // Punto donde aparecerá la gema

    private void Update()
    {
        // Elimina enemigos muertos de la lista
        enemies.RemoveAll(e => e == null);

        // Si ya no quedan enemigos vivos
        if (enemies.Count == 0 && emeraldPrefab != null)
        {
            SpawnEmerald();
            enabled = false; // Para que no siga intentando spawnear
        }
    }

    private void SpawnEmerald()
    {
        Instantiate(emeraldPrefab, spawnPoint.position, Quaternion.identity);
    }
}


public class EnemyManager : MonoBehaviour
{
    public int enemiesAlive = 0;
    private bool _spawnedEmerald;

    [Header("Spawn de esmeralda")]
    [SerializeField] private GameObject emeraldPrefab;
    [SerializeField] private float spawnHeight = 12f;   // altura de aparición
    [SerializeField] private float extraDownForce = 4f; // empuje hacia abajo
    [SerializeField] private Transform fallbackSpawnPoint;

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

        if (!_spawnedEmerald)
        {
            _spawnedEmerald = true;
            SpawnEmeraldAbovePlayer();
        }
    }

    private void SpawnEmeraldAbovePlayer()
    {
        if (emeraldPrefab == null)
        {
            Debug.LogError("[EnemyManager] emeraldPrefab no asignado.");
            return;
        }

        // Determinar posición base (jugador si existe, si no fallback)
        Transform player = FindPlayerTransform();
        Vector3 basePos = player != null ? player.position :
                          (fallbackSpawnPoint ? fallbackSpawnPoint.position : Vector3.zero);

        Vector3 spawnPos = basePos + Vector3.up * spawnHeight;

        GameObject emerald = Instantiate(emeraldPrefab, spawnPos, Quaternion.identity);

        // Asegurar caída “bonita”
        if (emerald.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.down * extraDownForce, ForceMode.VelocityChange);
        }

        if (emerald.TryGetComponent<Animator>(out var anim))
            anim.SetTrigger("Fall");
    }

    private Transform FindPlayerTransform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player ? player.transform : null;
    }
}*/