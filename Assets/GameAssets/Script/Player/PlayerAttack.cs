using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttcks;
    public float startTimeBetweenAttacks;

    GamepadControls controls;
    public Transform attackPos;
    public LayerMask whatIsAttackable;
    public float attckRange;
    public int damage;
    public GameObject bar;
    public Animator animator;

    private void Awake()
    {
        controls = new GamepadControls();
    }

    void Update()
    {
        if(timeBtwAttcks <= 0)
        {
            controls.Gameplay.Attack.performed += ctx => Attack();
            timeBtwAttcks = startTimeBetweenAttacks;
        }
        else
        {
            timeBtwAttcks -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attckRange);
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Attack()
    {
        animator.SetTrigger("Attacking");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attckRange, whatIsAttackable);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Destructable>().TakeDamage(damage);
        }
    }
}
