using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour {

	public Image logo;

	public float speed;
	public float yLimit;
	public float timeUntilFade;
	public float fadeRate;
	public float fadeLimit;

	public IEnumerator RunCredits()
	{		

		while(transform.position.y <= yLimit)
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed);
			yield return null;
		}	

		transform.position.Set(transform.position.x, yLimit, transform.position.z);

		yield return new WaitForSeconds(timeUntilFade);

		while (logo.color.a >= fadeLimit)
		{
			logo.color = new Vector4(1, 1, 1, logo.color.a - (fadeRate * Time.deltaTime));
			yield return null;
		}

		logo.color = new Vector4(1, 1, 1, fadeLimit);

	}

}
