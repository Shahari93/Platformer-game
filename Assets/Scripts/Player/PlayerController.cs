using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB = null;
    [SerializeField] private SpriteRenderer playerSR = null;
    [SerializeField] private Animator playerAnimator = null;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(-5f, playerRB.velocity.y);
            playerSR.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerRB.velocity = new Vector2(5f, playerRB.velocity.y);
            playerSR.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 10f);
        }    
    }
}
