using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EthanTheHero;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar_LR;    // Tu barra de vida en la UI
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar_LR.maxValue = maxHealth;
        healthBar_LR.value = currentHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Aquí actualizas la barra:
        healthBar_LR.value = currentHealth;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

void Die()
{
    Debug.Log("Player Died!");
    animator.SetBool("Death", true);
    GetComponent<EthanTheHero.PlayerMovement>().enabled = false;

    StartCoroutine(ShowMenuAfterDelay());
}

private IEnumerator ShowMenuAfterDelay()
{
    yield return new WaitForSeconds(1f); // Espera un segundo antes de mostrar el menú
    FindObjectOfType<MainMenuManager>().ShowGameOverMenu();
}
}
