using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float moveSpeed;
        
        
	void Update () {
        
        PlayerMove();
	}

    void PlayerMove()
    {

        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }

       

    }
    
    bool IsOnFloor()
    {
        if(Physics2D.Raycast((transform.position + ((transform.localScale/2).magnitude)*Vector3.down), Vector2.down, 0.01f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
