using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Init variables
    private Rigidbody2D playerRB = null;
    private SpriteRenderer playerSR = null;
    private Animator playerAnimator = null;
    private Collider2D playerColl = null;

    [Header("Variables")]
    [SerializeField] private Collider2D houseColl = null;
    [SerializeField] private int cherries = 0;
    [SerializeField] private LayerMask ground = 0;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private Text scoreText;
    [SerializeField] private int jumpAmount = 0;
    [SerializeField] private int extraJumpAmount = 1;
    [SerializeField] protected Image shrooms;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footStep = null;
    [SerializeField] private int healthAmount;
    [SerializeField] private Text healthText;

    //FSM
    private enum State { idle, running, jumping, fall, hurt }
    private State state = State.idle;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        playerColl = GetComponent<Collider2D>();
        healthText.text = healthAmount.ToString();
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

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }
    private void Jump()
    {
        if (jumpAmount < extraJumpAmount)
        {
            jumpAmount++;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            state = State.jumping;
        }
    }
    private void FootSteps()
    {
        footStep.Play();
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
                jumpAmount = 0;
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
            cherry.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            cherry.Play();
            Destroy(collision.gameObject);
            Color c = shrooms.color;
            c.a = 255f;
            shrooms.color = c;
            houseColl.isTrigger = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (state == State.fall)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                HandleHealth();
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

    private void HandleHealth()
    {
        healthAmount--;
        healthText.text = healthAmount.ToString();
        if (healthAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
