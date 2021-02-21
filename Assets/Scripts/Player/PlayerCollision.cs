using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    public bool hasKey = false;
    public int coinAmount = 0;
    public int lives = 3;
    [SerializeField] private Image[] hearts;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives--;
            hearts[lives].gameObject.SetActive(false);
            if (lives <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            hasKey = true;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coinAmount += 10;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Destroy(this.gameObject);
        }
    }
}
