using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float moveSpeed;
    public float speedCap;

    public float deacelerator;

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

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(this.gameObject);
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

        if(!Input.GetKey("a") && !Input.GetKey("d"))
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
        
}
