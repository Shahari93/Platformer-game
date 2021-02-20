using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttacks;
    public float startTimeBtwAttack;
    [SerializeField] private Animator anim;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;

    private void Update()
    {
        if (timeBtwAttacks <= 0)
        {
            //can attack

            if (Input.GetKey(KeyCode.Mouse0))
            {
                anim.SetBool("IsAttacking",true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyPatrol>().health -= damage;
                }
            }
            timeBtwAttacks = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
            anim.SetBool("IsAttacking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
