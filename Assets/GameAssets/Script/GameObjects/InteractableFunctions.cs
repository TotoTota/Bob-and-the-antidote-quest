using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFunctions : MonoBehaviour
{
    public Animator animator;

    private static InteractableFunctions instance;
    public bool interactableIsShowing { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public static InteractableFunctions GetInstance()
    {
        return instance;
    }

    public void Interact()
    {
        animator.SetBool("isInteracting", true);
        interactableIsShowing = true;

    }
    public void RemoveInteract()
    {
        animator.SetBool("isInteracting", false);
        interactableIsShowing = false;
    }
}
