using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHubExitScript : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerScript>() != null)
		{
			StageManagerScript.instance.LoadStage("FinalScene");
			collision.GetComponent<Collider2D>().enabled = false;
		}
	}
}
