﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    private bool lockedMovement = true;

    public float moveSpeed;
    private float currentSpeed = 0;
    public float speedCap;

    public float deacelerator;

    private Animator animator;
    private SpriteRenderer sr;

    private Rigidbody2D rb;
    public Collider2D hitBox;

    public Vector3 hitZoneOffset;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update () {
                
        PlayerMove();
        UpdateAnimator();
        EndlessPit();
        Suicide();
      
	}

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Destroy(this.gameObject);
    }

    void PlayerMove()
    {                

        if (Input.GetKey("left"))
        {
            if(rb.velocity.x > -speedCap)
            {
                rb.velocity += currentSpeed * Time.deltaTime * Vector2.left;
            }
        }

        if (Input.GetKey("right"))
        {
            if (rb.velocity.x < speedCap)
            {
                rb.velocity += currentSpeed * Time.deltaTime * Vector2.right;
            }
        }

        if(!Input.GetKey("left") && !Input.GetKey("right"))
        {
            MovimentStoper();
        }  

    }

    private void MovimentStoper()
    {
        float a = 0;

        if(rb.velocity.x > 0)
        {
            a = -1;
        }
        
        if(rb.velocity.x < 0)
        {
            a = 1;
        }

        rb.velocity += Time.deltaTime * a * Vector2.right * deacelerator;

        if (Mathf.Abs(rb.velocity.x) < 0.2f){
            rb.velocity = (new Vector2(0.0f, rb.velocity.y));
        }

    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsRunning", (Mathf.Abs(rb.velocity.x) > 1.0f));        
        sr.flipX = (rb.velocity.x < 0);
    }
        
    private void EndlessPit()
    {
        if(transform.position.y <= -30.0f)
        {
            Die();
        }
    }

    private void Suicide()
    {
        if(Input.GetKey("r")){
            Die();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (lockedMovement)
        {
            lockedMovement = false;
            currentSpeed = moveSpeed;
        }

        if(collision.gameObject.tag =="Enemy")
        {
            

            foreach (var contact in collision.contacts)
            {
                
                if (transform.position.y + hitZoneOffset.y < contact.point.y)
                {
                    Die();
                    return;
                }
            }

            collision.gameObject.GetComponent<EnemyScript>().PlayerKill(gameObject);
        }
    }
}
