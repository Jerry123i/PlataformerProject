using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GravityState {NORMAL, FALL, LOWJUMP}
public class PlayerJump : MonoBehaviour {

    [Range(1, 10)]
              
    public float jumpVelocity;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    [SerializeField]
    private GravityState gravityState;
    private float gravityMultiplier;

    private float terminalVelocity = 18.0f;
    
    private Animator animator;

    private PlayerScript player;

    Rigidbody2D rb;

    private void Awake()
    {
        gravityState = GravityState.NORMAL;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerScript>();
    }

    private void Update()
    {

        if (!player.LockedMovement)
        {
            if (Input.GetButtonDown("Jump") && IsOnFloor())
            {
                rb.velocity += Vector2.up * jumpVelocity;
            }

        }

        animator.SetBool("IsJumping", !IsOnFloor());
        GravitySetter();
        TerminalVelocity();
             
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

    private void TerminalVelocity()
    {
        if(rb.velocity.y < -terminalVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -terminalVelocity);
        }
    }

    private void GravitySetter()
    {

        if (IsOnFloor())
        {
            gravityState = GravityState.NORMAL;
        }

        if (rb.velocity.y < 0)
        {
            gravityState = GravityState.FALL;
        }
        if (rb.velocity.y > 0 && !(Input.GetButton("Jump")))
        {
            gravityState = GravityState.LOWJUMP;
        }

        switch (gravityState)
        {

            case GravityState.NORMAL:
                gravityMultiplier = 1.0f;
                break;
            case GravityState.FALL:
                gravityMultiplier = fallMultiplier;
                break;
            case GravityState.LOWJUMP:
                gravityMultiplier = lowJumpMultiplier;
                break;

        }

        rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1.0f) * Time.deltaTime;


    }


}
