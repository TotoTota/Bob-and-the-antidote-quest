using System.Collections;
using UnityEngine;

public class OpenOnTrigger : MonoBehaviour
{
    public PlayerMovemnt playerMovemnt;
    public Animator wallAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovemnt.speed = 0;
            StartCoroutine(OpenWall());
        }
    }

     public IEnumerator OpenWall()
    {
        wallAnimator.SetBool("closeIsClose", false);
        yield return new WaitForSeconds(1f);
        playerMovemnt.speed = 5;
    }
}
