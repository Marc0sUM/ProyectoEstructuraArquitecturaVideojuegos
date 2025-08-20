using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemigos")]
    private List<Enemy> enemies = new List<Enemy>();
    private bool _spawnedEmerald;

    [Header("Spawn de esmeralda")]
    [SerializeField] private GameObject emeraldPrefab;
    [SerializeField] private float spawnHeight = 24f;
    [SerializeField] private float skinOffset = 0.25f;
    [SerializeField] private float extraDownForce = 1f;
    [SerializeField] private Transform fallbackSpawnPoint;
    [SerializeField] private LayerMask groundMask = ~0;

    [Header("Modo Test (completar por kills)")]
    [Tooltip("Si está activo, el nivel se completa tras killsToComplete enemigos derrotados.")]
    [SerializeField] private bool testModeCompleteOnKill = false;

    [Tooltip("Cantidad de enemigos necesarios para completar en modo test.")]
    [SerializeField] private int killsToComplete = 1;

    private int killsSoFar = 0;

    [Header("Eventos")]
    public UnityEvent OnLevelCompleted; // opcional, para UI/sonidos/etc.

     public event Action<int> OnRemainingChanged;

     private void UpdateEnemiesUI()
    {
        int remaining = testModeCompleteOnKill
            ? Mathf.Max(0, killsToComplete - killsSoFar)
            : enemies.Count;

        OnRemainingChanged?.Invoke(remaining);
    }

    void Start()
    {
        Debug.Log("EnemyManager listo");
        UpdateEnemiesUI();
    }

    private void Update()
    {
        // Comportamiento normal: si ya no quedan enemigos vivos
        if (!testModeCompleteOnKill && enemies.Count <= 0 && !_spawnedEmerald)
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

    if (testModeCompleteOnKill && !_spawnedEmerald)
    {
        killsSoFar++;
        UpdateEnemiesUI();  // <- refleja faltantes en modo test

        if (killsSoFar >= Mathf.Max(1, killsToComplete))
        {
            LevelComplete();
            return;
        }
    }
    else
    {
        UpdateEnemiesUI();  // <- refleja faltantes en modo normal
    }

    if (!testModeCompleteOnKill && enemies.Count <= 0)
    {
        LevelComplete();
    }
    }

    // ==== Spawning de la esmeralda ====

    private void LevelComplete()
    {
        if (_spawnedEmerald) return;
        UpdateEnemiesUI();
        _spawnedEmerald = true;
        Debug.Log("VICTORIA! Nivel completado.");
        SpawnEmeraldAbovePlayer();

        // Dispara evento para UI/sonido/score/etc.
        OnLevelCompleted?.Invoke();
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

    // Origen del raycast: base + altura
    Vector3 probeStart = basePos + Vector3.up * spawnHeight;

    // Raycast hacia abajo: una única forma de decidir el spawn
    RaycastHit2D hit = Physics2D.Raycast(probeStart, Vector2.down, Mathf.Infinity, groundMask);

    float x = probeStart.x;
    float y = hit.collider ? (hit.point.y + skinOffset) : probeStart.y;
    Vector3 spawnPos = new Vector3(x, y, 0f);

    // Instanciar
    GameObject emerald = Instantiate(emeraldPrefab, spawnPos, Quaternion.identity);

    // (opcional) ajustes de físicas
    if (emerald.TryGetComponent<Rigidbody2D>(out var rb2d))
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 1f;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Physics2D.SyncTransforms();
        rb2d.AddForce(Vector2.down * extraDownForce, ForceMode2D.Impulse);
    }

    if (emerald.TryGetComponent<BoxCollider2D>(out var col))
        col.isTrigger = false;

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