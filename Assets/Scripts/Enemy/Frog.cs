using UnityEngine;

public class Frog : MonoBehaviour
{
    [Header("Jump Variables")]
    [SerializeField] private float leftCap = 0f;
    [SerializeField] private float rightCap = 0f;
    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool facingLeft = true;
    private Collider2D frogColl;
    private Rigidbody2D frogRB;


    private void Start()
    {
        frogColl = GetComponent<Collider2D>();
        frogRB = GetComponent<Rigidbody2D>();
    }

    public void Update()
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
                    frogRB.velocity = new Vector2(-jumpLength, jumpHeight);
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
                    frogRB.velocity = new Vector2(jumpLength, jumpHeight);
                }
            }

            else
            {
                facingLeft = true;
            }
        }
    }
}
