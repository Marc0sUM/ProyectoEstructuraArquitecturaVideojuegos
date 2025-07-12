using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public Collider2D attackHitbox;

    void Start()
    {
        attackHitbox.enabled = false;
    }

    public void EnableHitbox()
{
    attackHitbox.enabled = true;
    Debug.Log("HITBOX ACTIVADA");
}

public void DisableHitbox()
{
    attackHitbox.enabled = false;
    Debug.Log("HITBOX DESACTIVADA");
}
private void OnTriggerEnter2D(Collider2D other)
{
   if (other.CompareTag("Enemy"))
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Calcula la dirección del empuje (del player al enemigo)
            Vector2 knockbackDir = (other.transform.position - transform.position).normalized;

            enemyHealth.TakeDamage(20, knockbackDir);
        }
    }
}
}
