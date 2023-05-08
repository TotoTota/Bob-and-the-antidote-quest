using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovemnt : MonoBehaviour
{
    private float horizontal;
    public float speed = 5f;
    public float jumpForce = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool knockFromRight;

    public Animator animator;

    public GamepadControls controls;

    void Awake()
    {
        controls = new GamepadControls();

        controls.Gameplay.Jump.performed += ctx => Jump();
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

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetFloat("Walk", 0);
            animator.SetBool("isJumping", false);
            speed = 0;
        }
        else
        {
            animator.SetFloat("Walk", 0);
            speed = 5;
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

    void Jump()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void Flip()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
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
