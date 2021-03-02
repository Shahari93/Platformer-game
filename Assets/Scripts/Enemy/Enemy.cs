﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator frogAnim;
    protected Rigidbody2D enemyRB;
    protected AudioSource deathSFX;
    protected virtual void Start()
    {
        frogAnim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        deathSFX = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        frogAnim.SetTrigger("IsDead");
        deathSFX.Play();
        enemyRB.velocity = Vector2.zero;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
