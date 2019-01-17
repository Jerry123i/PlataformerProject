using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroSceneScript : MonoBehaviour {

	public float yValue;

	public GameObject player;

	public PolygonCollider2D constraints;

	public CinemachineConfiner confiner;
	
	private void Awake()
	{
		Debug.Log(PlayerPrefs.GetInt("DiedOnIntro"));
		if(PlayerPrefs.GetInt("DiedOnIntro") > 0)
		{
			Debug.Log("Bop");
			player.transform.Translate(0, yValue, 0);
			confiner.m_BoundingShape2D = constraints;
		}
	}


}
