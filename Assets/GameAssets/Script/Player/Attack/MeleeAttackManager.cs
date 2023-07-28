using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackManager : MonoBehaviour
{
    public float defaultForce = 300;
    public float upwardsForce = 600;
    public float movementTime = 0.1f;
    private bool meleeAttack;
    private Animator meleeAnimator;
    private Animator anim;
    private PlayerMovemnt player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMovemnt>();
        meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (InputManager.GetInstance().GetAttackPressed())
        {
            meleeAttack = true;
        }
        else
        {
            meleeAttack = false;
        }

        if(meleeAttack && Input.GetAxis("Vertical") > 0)
        {
            anim.SetTrigger("UpwardMelee");
            meleeAnimator.SetTrigger("UpwardMeleeSwipe");
        }

        if(meleeAttack && Input.GetAxis("Vertical") < 0 && !player.isGrounded())
        {
            anim.SetTrigger("DownwardMelee");
            meleeAnimator.SetTrigger("DownwardMeleeSwipe");
        }

        if((meleeAttack && Input.GetAxis("Vertical") == 0) || meleeAttack && (Input.GetAxis("Vertical") < 0 && player.isGrounded()))
        {
            anim.SetTrigger("ForwardMelee");
            meleeAnimator.SetTrigger("ForwardMeleeSwipe");
        }
    }
}
