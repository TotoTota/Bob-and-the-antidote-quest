using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemnt : MonoBehaviour
{
    private float horizontal;
    public float speed = 5f;
    public float jumpForce = 16f;
    public bool isFacingRight = true;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool knockFromRight;

    private AudioSource audioSource;
    public AudioClip jumpSound;

    public Animator animator;

    public GamepadControls controls;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        controls = new GamepadControls();
    }

    void FixedUpdate()
    {
        if(WallBehaviour.playForStop == false)
        {
            rb.gravityScale = 0;
            return;
        }
        else
        {
            rb.gravityScale = 4;
        }

        if (DialogueManager.GetInstance().dialogueIsPlaying || InteractableFunctions.GetInstance().interactableIsShowing)
        {
            animator.SetFloat("Walk", 0);
            animator.SetBool("isJumping", false);
            speed = 0;
        }
        else
        {
            animator.SetFloat("Walk", 0);
            speed = 7f;
        }

        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            if(knockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce / 2);
            }
            if(knockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce / 2);
            }

            KBCounter -= Time.deltaTime;
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (DialogueManager.GetInstance().dialogueIsPlaying || InteractableFunctions.GetInstance().interactableIsShowing)
        {
            return;
        }

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (InputManager.GetInstance().GetJumpPressed())
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("IsFalling?", true);
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            audioSource.clip = jumpSound;
            audioSource.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            animator.SetBool("IsFalling?", false);
            jumpBufferCounter = 0f;
        }

        if (rb.velocity.y > 0 && InputManager.GetInstance().GetJumpPressed())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("IsFalling?", true);
        }

        if (rb.velocity.y == 0 && isGrounded())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("IsFalling?", false);
        }

        Flip();

    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void Flip()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying || InteractableFunctions.GetInstance().interactableIsShowing)
        {
            return;
        }

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        animator.SetFloat("Walk", Mathf.Abs(horizontal));
    }
}
