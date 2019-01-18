using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicControler : MonoBehaviour {

    //public AudioSource insideSource;
    //public AudioSource outsideSource;
    //public AudioClip insideMusic;
    //public AudioClip voidMusic;

    //float changeMusicRate = 0.7f;

    private void Awake()
    {
        //insideSource.clip = insideMusic;
        //outsideSource.clip = voidMusic;

        //insideSource.Play();
        //outsideSource.Play();

        //insideSource.volume = 0;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerScript>() != null)
        {
			Debug.Log("BG enter");
			StageManagerScript.instance.audioManager.SoundShift(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>() != null)
        {
			Debug.Log("BG exit");
			StageManagerScript.instance.audioManager.SoundShift(false);
		}
    }


    //IEnumerator EnterFrame()
    //{
    //    while (insideSource.volume != 1 && outsideSource.volume != 0)
    //    {
    //        insideSource.volume += changeMusicRate*Time.deltaTime;
    //        outsideSource.volume -= changeMusicRate*Time.deltaTime;
    //        yield return null;
    //    }

    //}

    //IEnumerator ExitFrame()
    //{

    //    while(insideSource.volume != 0 && outsideSource.volume != 1)
    //    {
    //        insideSource.volume -= changeMusicRate*Time.deltaTime;
    //        outsideSource.volume += changeMusicRate*Time.deltaTime;
    //        yield return null;
    //    }

    //}

}
