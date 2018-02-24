using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float moveSpeed;
    public float speedCap;
        
        
	void Update () {
        
        PlayerMove();
               
	}

    void PlayerMove()
    {

        Rigidbody2D rb;

        rb = this.GetComponent<Rigidbody2D>();

        if (Input.GetKey("a"))
        {
            if(rb.velocity.x > -speedCap)
            {
                rb.velocity += moveSpeed * Time.deltaTime * Vector2.left;
            }
        }

        if (Input.GetKey("d"))
        {
            if (rb.velocity.x < speedCap)
            {
                rb.velocity += moveSpeed * Time.deltaTime * Vector2.right;
            }
        }

       

    }
        
}
