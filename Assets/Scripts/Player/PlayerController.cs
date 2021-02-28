using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Init variables
    private Rigidbody2D playerRB = null;
    private SpriteRenderer playerSR = null;
    private Animator playerAnimator = null;
    private Collider2D playerColl = null;

    [Header("Variables")]
    [SerializeField] private int cherries = 0;
    [SerializeField] private LayerMask ground = 0;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private Text scoreText;

    //FSM
    private enum State { idle, running, jumping, fall, hurt }
    private State state = State.idle;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        playerColl = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }

        AnimationSwitchStates();
        playerAnimator.SetInteger("State", (int)state); // sets animations based on enum states
    }

    private void Movement()
    {
        float horMov = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (horMov < 0)
        {
            playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
            playerSR.flipX = true;

        }
        else if (horMov > 0)
        {
            playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
            playerSR.flipX = false;

        }
        if (Input.GetButtonDown("Jump") && playerColl.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationSwitchStates()
    {
        if (state == State.jumping)
        {
            if (playerRB.velocity.y < .1f)
            {
                state = State.fall;
            }
        }
        else if (state == State.fall)
        {
            if (playerColl.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(playerRB.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(playerRB.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            cherries++;
            scoreText.text = cherries.ToString();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (state == State.fall)
            {
                Destroy(collision.gameObject);
                Jump();
            }
            else
            {
                state = State.hurt;
                if (collision.gameObject.transform.position.x > this.transform.position.x)
                {
                    playerRB.velocity = new Vector2(-hurtForce, playerRB.velocity.y);
                }
                else
                {
                    playerRB.velocity = new Vector2(hurtForce, playerRB.velocity.y);
                }
            }
        }
    }
}
