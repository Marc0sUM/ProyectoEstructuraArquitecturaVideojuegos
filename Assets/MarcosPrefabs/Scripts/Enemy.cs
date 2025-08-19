using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float chaseDistance = 5f;

    [SerializeField] private EnemyManager enemyManager;
    public int health = 50;

    void Start()
    {
        if (enemyManager == null)
            enemyManager = FindObjectOfType<EnemyManager>();

        if (enemyManager != null)
            enemyManager.AddEnemy(this);
        else
            Debug.LogError("EnemyManager no encontrado para " + gameObject.name);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " murió!");

        if (enemyManager != null)
        {
            enemyManager.EnemyDefeated(this);
        }

        Destroy(gameObject);
    }
}


/*using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float chaseDistance = 5f;

    [SerializeField] private EnemyManager enemyManager;
    public int health = 50;

    void Start()
    {
        // Si no está asignado en el inspector, lo buscamos en la escena
        if (enemyManager == null)
            enemyManager = FindObjectOfType<EnemyManager>();

        if (enemyManager != null)
            enemyManager.AddEnemy(this);
        else
            Debug.LogError("EnemyManager no encontrado para " + gameObject.name);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " murió!");

        if (enemyManager != null)
        {
            enemyManager.EnemyDefeated(this); // le pasamos la referencia del enemigo muerto
        }

        Destroy(gameObject);
    }
}  */