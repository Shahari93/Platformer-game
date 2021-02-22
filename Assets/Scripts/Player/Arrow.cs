using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D arrowRB;
    BoxCollider2D arrowColl;
    bool hasHits;
    private void Start()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        arrowColl = GetComponent<BoxCollider2D>();
    }

    //private void Update()
    //{
    //    if (!hasHits)
    //    {
    //        float angle = Mathf.Atan2(arrowRB.velocity.y, arrowRB.velocity.x) * Mathf.Rad2Deg;
    //        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHits = true;
        arrowRB.velocity = Vector2.zero;
        arrowRB.isKinematic = true;
        arrowColl.isTrigger = true;
    }
}
