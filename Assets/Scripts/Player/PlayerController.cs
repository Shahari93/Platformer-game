using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Init variables
    private Rigidbody2D playerRB = null;
    private SpriteRenderer playerSR = null;
    private Animator playerAnimator = null;
    private Collider2D playerColl = null;

    [Header("Variables")]
    [SerializeField] private LayerMask ground = 0;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    //FSM
    private enum State { idle, running, jumping, fall }
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
        Movement();

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
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            state = State.jumping;
        }
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
        else if (Mathf.Abs(playerRB.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
