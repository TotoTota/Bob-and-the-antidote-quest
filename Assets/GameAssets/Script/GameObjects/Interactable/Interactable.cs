using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public UnityEvent interactionAction;
    public UnityEvent interactionOff;
    public Animator yButton;
    public bool isInRange;
    public GameObject activeUi;
    public GameObject activeButtonY;
    GamepadControls controls;

    private void Awake()
    {
        controls = new GamepadControls();
    }

    private void Update()
    {
        if (isInRange)
        {
            controls.Gameplay.Enable();
            controls.Gameplay.Interact.performed += ctx => interactionAction.Invoke();
            controls.Gameplay.RemoveInteract.performed += ctx => interactionOff.Invoke();
        }
        else if(!isInRange)
        {
            controls.Gameplay.Disable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activeUi.SetActive(true);
        isInRange = true;
        activeButtonY.SetActive(true);
        yButton.SetBool("YOpen?", true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeUi.SetActive(false);
        isInRange = false;
        yButton.SetBool("YOpen?", false);
        StartCoroutine(WaitBeforeDeactivate(0.25f));
    }

    IEnumerator WaitBeforeDeactivate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activeButtonY.SetActive(false);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
