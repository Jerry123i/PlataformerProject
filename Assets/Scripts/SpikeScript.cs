using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour {

	private void Awake()
	{
		gameObject.layer = 2; //IgnoreRaycast
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>())
        {
            Debug.Log("Kill");
            collision.GetComponent<PlayerScript>().Die();
        }
    }

}
