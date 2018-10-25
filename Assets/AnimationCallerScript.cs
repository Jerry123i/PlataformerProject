﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallerScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            collision.gameObject.GetComponent<PlayerScript>().StartCoroutine(collision.gameObject.GetComponent<PlayerScript>().StartFallAnimation());
            collision.GetComponent<Rigidbody2D>().gravityScale = 1.3f;
            Destroy(gameObject);
        }
    }

}
