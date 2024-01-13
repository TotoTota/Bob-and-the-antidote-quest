using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    private float horizontal;
    public float speed = 5f;
    public float jumpForce = 16f;
    public float startJumpDelay;
    private float jumpDelay;
    public bool isFacingRight = true;
    public float checkRadius;
    public PhysicsMaterial2D highFirction;
    public PhysicsMaterial2D lowFriction;
    public PhysicsMaterial2D walkFriction;

    public Collider2D collider1;

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

    public float TimeBtwTrail;
    public GameObject trailEffectGround;
    public float StartTimeTrail;
    public GameObject jumpEffectGround;

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

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider1.bounds.center, collider1.bounds.size, 0, Vector2.down, 0.2f, groundLayer);

        return hit.collider != null;
    }

    private bool isFalling()
    {
        if (rb.velocity.y < 0 && !isGrounded())
        {
            return true;
        }

        return false;
    }

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

        if(horizontal != 0)
        {
            rb.sharedMaterial = lowFriction;
        }
        else
        {
            rb.sharedMaterial = highFirction;
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

        if(horizontal != 0 && isGrounded())
        {
            if(TimeBtwTrail <= 0)
            {
                Instantiate(trailEffectGround, groundCheck.position, Quaternion.identity);
                TimeBtwTrail = StartTimeTrail;
            }
            else
            {
                TimeBtwTrail -= Time.deltaTime;
            }
        }

        if (isFalling())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("IsFalling?", true);
        }

        if (!isFalling())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("IsFalling?", false);
        }

        if (isGrounded() && InputManager.GetInstance().GetJumpPressed() && jumpDelay <= 0)
        {
            jumpDelay = startJumpDelay;
            animator.SetBool("isJumping", true);
            animator.SetBool("IsFalling?", false);
            audioSource.clip = jumpSound;
            audioSource.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Instantiate(jumpEffectGround, groundCheck.position, Quaternion.identity);
        }
        else
        {
            jumpDelay -= Time.deltaTime;
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
}
