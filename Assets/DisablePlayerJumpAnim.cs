using UnityEngine;

public class DisablePlayerJumpAnim : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("IsFalling?", false);
    }
}
