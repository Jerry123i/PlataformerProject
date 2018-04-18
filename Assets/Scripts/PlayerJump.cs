using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    [Range(1, 10)]
    public float jumpVelocity;

    public float fallMultiplier;
    public float lowJumpMultiplier;
    
    
    private Animator animator;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        

        if (Input.GetButtonDown("Jump") && IsOnFloor())
        {            
            rb.velocity += Vector2.up * jumpVelocity;            
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1.0f) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !(Input.GetButton("Jump"))){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1.0f) * Time.deltaTime;
        }

        animator.SetBool("IsJumping", !IsOnFloor());

    }

    bool IsOnFloor()        
    {
        RaycastHit2D[] boxResult;
        bool boolResult = false;

        boxResult = Physics2D.BoxCastAll(new Vector2(transform.position.x + GetComponent<BoxCollider2D>().offset.x, transform.position.y + GetComponent<BoxCollider2D>().offset.y), new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y/2), 0.0f, Vector2.down, GetComponent<BoxCollider2D>().size.y/2);
        
        for (int i = 0; i<boxResult.Length; i++){

            GameObject resultGO = boxResult[i].collider.gameObject;

            if (resultGO.layer != LayerMask.NameToLayer("Player"))
            {

                if (resultGO.tag == "Enemy")
                {
                    Debug.Log("Enemy");
                    return false;
                }


                if (resultGO.GetComponent<PassTileScript>() != null)
                {
                    if (resultGO.GetComponent<PassTileScript>().playerInside)
                    {
                        return false;
                    }
                }

                boolResult = true;

                
            }
        }

        return boolResult;
                
    }


}
