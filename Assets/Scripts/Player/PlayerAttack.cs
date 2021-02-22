using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject bow;
    [SerializeField] private Animator anim;
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    private float timeBtwAttacks;
    public float startTimeBtwAttack;
    public float attackRange;
    public int damage;


    private void Start()
    {
        bow.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bow.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bow.SetActive(true);
        }

        if (timeBtwAttacks <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !bow.gameObject.activeInHierarchy)
            {
                anim.SetBool("IsAttacking", true);
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
