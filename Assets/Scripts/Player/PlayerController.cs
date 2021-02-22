using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private bool facineRight = true;
    public float playerSpeed;
    public float jumpForce;
    private float moveInput;
    private Rigidbody2D playerRb;
    public Animator playerAnim;

    private bool isTouchingFront;
    private bool wallSlide;
    public Transform frontCheck;
    public float wallSlideSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private int extraJumps;
    public int extraJumpValue;
    public float checkRadius;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpValue;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        moveInput = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(moveInput * playerSpeed, playerRb.velocity.y);
        if (!facineRight && moveInput > 0)
        {
            Flip();
        }
        else if (facineRight && moveInput < 0)
        {
            Flip();
        }
        if (moveInput < 0 || moveInput > 0)
        {
            playerAnim.SetBool("IsRunning", true);
        }
        else
            playerAnim.SetBool("IsRunning", false);
    }

    private void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpValue;
            playerAnim.SetBool("IsJumping", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            playerAnim.SetBool("IsJumping", true);
            playerRb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            playerRb.velocity = Vector2.up * jumpForce;
            playerAnim.SetBool("IsJumping", false);
        }
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundLayer);
        if (isTouchingFront && !isGrounded && moveInput != 0)
        {
            wallSlide = true;
        }
        else
        {
            wallSlide = false;
        }

        if (wallSlide)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Clamp(playerRb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallSlide)
        {
            wallJumping = true;
            Invoke("SetWallJumpToFalse", wallJumpTime);
        }
        if(wallJumping)
        {
            playerRb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }
    }

    void Flip()
    {
        facineRight = !facineRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    void SetWallJumpToFalse()
    {
        wallJumping = false;
    }
}