using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public int vidaExtra = 50;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            if(health != null){
                health.AumentarVida(vidaExtra);
            }
            Destroy(gameObject);
        }
    }
}
