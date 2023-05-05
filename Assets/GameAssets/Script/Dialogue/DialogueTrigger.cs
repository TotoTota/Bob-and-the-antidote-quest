using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;

    [Header("Input")]
    private GamepadControls controls;

    private bool playerInRange;

    public Animator animator;

    private void Awake()
    {
        controls = new GamepadControls();
        playerInRange = false;
        visualCue.SetActive(false);
        animator.SetBool("YOpen?", false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            controls.Gameplay.Enable();
            visualCue.SetActive(true);
            animator.SetBool("YOpen?", true);
            controls.Gameplay.Interact.performed += ctx => Interact(); 
        }
        else
        {
            controls.Gameplay.Disable();
            animator.SetBool("YOpen?", false);
            StartCoroutine(WaitBeforeClose(0.25f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Interact()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJson);
    }

    IEnumerator WaitBeforeClose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        visualCue.SetActive(false);
    }
}
