using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

	private float changeMusicRate = 1.0f;

	public AudioSource sourceA;
	public AudioSource sourceB;

	public int currentWorld;

	[Header("Mundo 1")]
	public AudioClip w1Start;
	public AudioClip w1Loop;
	public AudioClip w1StartB;
	public AudioClip w1LoopB;

	[Header("Mundo 2")]
	public AudioClip w2Start;
	public AudioClip w2Loop;
	public AudioClip w2StartB;
	public AudioClip w2LoopB;

	[Header("Mundo 3")]
	public AudioClip w3Start;
	public AudioClip w3Loop;
	public AudioClip w3StartB;
	public AudioClip w3LoopB;

	public void StartMusic(int i)
	{
		currentWorld = i;

		switch (currentWorld)
		{
			case 0:
				PauseBoth();
				break;

			case 1:
				StartCoroutine(StartAndEnterLoop(w1Start, w1StartB, w1Loop, w1LoopB));
				break;

			case 2:
				StartCoroutine(StartAndEnterLoop(w2Start, w2StartB, w2Loop, w2LoopB));
				break;

			case 3:
				StartCoroutine(StartAndEnterLoop(w3Start, w3StartB, w3Loop, w3LoopB));
				break;
		}

	}
	
	IEnumerator StartAndEnterLoop(AudioClip audioAStart, AudioClip audioBStart, AudioClip audioALoop, AudioClip audioBLoop)
	{
		if(audioAStart.length != audioBStart.length || audioALoop.length != audioBLoop.length)
		{
			Debug.LogWarning("Diferent legth audio clips!");
		}

		sourceA.clip = audioAStart;
		sourceB.clip = audioBStart;

		PlayBoth();

		yield return new WaitForSeconds(audioAStart.length);

		sourceA.clip = audioALoop;
		sourceB.clip = audioBLoop;

		sourceA.loop = true;
		sourceB.loop = true;

		PlayBoth();

	}

	public void PlayBoth()
	{
		sourceA.Play();
		sourceB.Play();
	}

	public void PlayBoth(int i)
	{
		PlayBoth();

		if(i == 1)
		{
			sourceA.volume = 1;
			sourceB.volume = 0;
		}
		else
		{
			sourceA.volume = 0;
			sourceB.volume = 1;
		}
	}

	public void PauseBoth()
	{
		sourceA.Pause();
		sourceB.Pause();
	}


	IEnumerator EnterFrame()
	{
		while (sourceA.volume != 1 && sourceB.volume != 0)
		{
			sourceA.volume += changeMusicRate * Time.deltaTime;
			sourceB.volume -= changeMusicRate * Time.deltaTime;
			yield return null;
		}

	}

	IEnumerator ExitFrame()
	{

		while (sourceA.volume != 0 && sourceB.volume != 1)
		{
			sourceA.volume -= changeMusicRate * Time.deltaTime;
			sourceB.volume += changeMusicRate * Time.deltaTime;
			yield return null;
		}

	}




}

