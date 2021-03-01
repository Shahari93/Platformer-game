using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator frogAnim;
    protected Rigidbody2D enemyRB;
    protected virtual void Start()
    {
        frogAnim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    public void JumpedOn()
    {
        frogAnim.SetTrigger("IsDead");
        enemyRB.velocity = Vector2.zero;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
