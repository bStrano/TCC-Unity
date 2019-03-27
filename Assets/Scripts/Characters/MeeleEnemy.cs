using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleEnemy : CEnemy
{
    [SerializeField] private float attackSpeed;

//    [SerializeField] private AudioSource audioSource;
//    [SerializeField] private string attackClip;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float attackRange;

    // Use this for initialization
    void Start()
    {
        base.Start();
        //audioSource = GetComponent<AudioSource>();
//        InvokeRepeating("Attack", 0, attackSpeed);
    }

    void Update()
    {
        base.Update();
    }


    protected override void Attack()
    {
        Debug.Log("Attack");
        if (!isDead)
        {
            animator.SetBool("Slash", true);
            bool sucessHit = false;

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
            foreach (Collider2D enemy in enemiesToDamage)
            {
                sucessHit = true;
                enemy.GetComponent<Player>().TakeDamage(damage, "successHit");
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}