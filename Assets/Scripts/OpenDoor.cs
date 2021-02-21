using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private PlayerCollision playerColl;
    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (playerColl.hasKey)
        {
            boxCollider.isTrigger = true;
        }
    }
}
