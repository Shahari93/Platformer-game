using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTutorials : MonoBehaviour
{
    [SerializeField] private GameObject panel = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            panel.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.gameObject.SetActive(false);
        }
    }
}
