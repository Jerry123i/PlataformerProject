using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    [Range(1, 10)]
    public float jumpVelocity;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Input.GetKeyDown("w"))
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1.0f) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !(Input.GetKey("w"))){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1.0f) * Time.deltaTime;
        }
    }
    

}
