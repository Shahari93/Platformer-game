using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float jumpForce;
    private float moveInput;
    public float checkRadius;
    private int extraJumps;
    public int extraJumpValue;
    private Rigidbody2D playerRb;
    private bool facineRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator playerAnim;

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
        if(moveInput<0||moveInput>0)
        {
            playerAnim.SetBool("IsRunning",true);
        }
        else
            playerAnim.SetBool("IsRunning",false);
    }

    private void Update()
    {
        if(isGrounded)
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
    }

    void Flip()
    {
        facineRight = !facineRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}