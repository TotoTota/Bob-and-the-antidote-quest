using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    public PlayerMovemnt playerMovemnt;
    public Health playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(1);
            playerMovemnt.KBCounter = playerMovemnt.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovemnt.knockFromRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerMovemnt.knockFromRight = false;
            }
        }
    }
}
