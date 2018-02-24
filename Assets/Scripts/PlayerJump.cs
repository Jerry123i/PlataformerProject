using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    [Range(1, 10)]
    public float jumpVelocity;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Input.GetKeyDown("w") && IsOnFloor())
        {
            rb.velocity += Vector2.up * jumpVelocity;
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1.0f) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !(Input.GetKey("w"))){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1.0f) * Time.deltaTime;
        }
    }

    bool IsOnFloor()
    {
        if (Physics2D.Raycast((transform.position + ((transform.localScale / 2).magnitude) * Vector3.down), Vector2.down, 0.01f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
