using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public int health = 2;
    public float speed;
    private bool isMovingRight = true;
    public Transform ground;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        int layer_mask = LayerMask.GetMask("Ground");
        RaycastHit2D groundInfo = Physics2D.Raycast(ground.position, Vector2.down, 2.0f, LayerMask.GetMask("Ground"));
        if (groundInfo.collider == false)
        {
            if (isMovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }

            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
        }
        if(health<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
