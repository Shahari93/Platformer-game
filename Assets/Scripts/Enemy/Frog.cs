using UnityEngine;

public class Frog : Enemy
{
    [Header("Jump Variables")]
    [SerializeField] private float leftCap = 0f;
    [SerializeField] private float rightCap = 0f;
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool facingLeft = true;
    private Collider2D frogColl;


    protected override void Start()
    {
        base.Start();
        frogColl = GetComponent<Collider2D>();
    }

    public void Update()
    {
        if (frogAnim.GetBool("IsJumping"))
        {
            if (enemyRB.velocity.y < .1f)
            {
                frogAnim.SetBool("IsFalling", true);
                frogAnim.SetBool("IsJumping", false);
            }
        }

        if (frogAnim.GetBool("IsFalling"))
        {
            if (frogColl.IsTouchingLayers(ground))
            {
                frogAnim.SetBool("IsFalling", false);
            }
        }
    }

    private void FrogMovement()
    {
        if (facingLeft)
        {
            //test to see if we are beyond the leftCap
            if (transform.position.x > leftCap)
            {
                // make sure sprite facing rigth direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                //test to see if the frog is on the ground, we can jump
                if (frogColl.IsTouchingLayers(ground))
                {
                    enemyRB.velocity = new Vector2(-jumpLength, jumpHeight);
                    frogAnim.SetBool("IsJumping", true);
                }
            }

            else
            {
                facingLeft = false;
            }
        }

        else
        {
            // make sure sprite facing rigth direction

            //test to see if we are beyond the leftCap
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                //test to see if the frog is on the ground, we can jump
                if (frogColl.IsTouchingLayers(ground))
                {
                    enemyRB.velocity = new Vector2(jumpLength, jumpHeight);
                    frogAnim.SetBool("IsJumping", true);
                }
            }

            else
            {
                facingLeft = true;
            }
        }
    }
}
