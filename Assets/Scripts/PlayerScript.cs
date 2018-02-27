using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float moveSpeed;
    public float speedCap;

    private Animator animator;
    private SpriteRenderer sr;

    private Rigidbody2D rb;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update () {
        
        PlayerMove();
        UpdateAnimator();
               
	}

    void PlayerMove()
    {
                

        if (Input.GetKey("a"))
        {
            if(rb.velocity.x > -speedCap)
            {
                //rb.AddForce(Vector2.left * moveSpeed * Time.deltaTime);
                rb.velocity += moveSpeed * Time.deltaTime * Vector2.left;
            }
        }

        if (Input.GetKey("d"))
        {
            if (rb.velocity.x < speedCap)
            {
                //rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime);
                rb.velocity += moveSpeed * Time.deltaTime * Vector2.right;
            }
        }

       

    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsRunning", (Mathf.Abs(rb.velocity.x) > 0.0f));        
        sr.flipX = (rb.velocity.x < 0);
    }
        
}
