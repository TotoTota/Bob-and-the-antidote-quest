using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFunctions : MonoBehaviour
{
    public Animator animator;
    public PlayerMovemnt playermovement;

    public void Interact()
    {
        animator.SetBool("isInteracting", true);
        playermovement.speed = 0f;
        playermovement.animator.enabled = false;

    }
    public void RemoveInteract()
    {
        animator.SetBool("isInteracting", false);
        playermovement.speed = 5f;
        playermovement.animator.enabled = true;
    }
}
