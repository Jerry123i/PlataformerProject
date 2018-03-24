using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeakTileScript : MonoBehaviour {

    bool isShaking;
    float time;
    float maxTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isShaking)
        {
            StartCoroutine(StartShake(1.5f));
        }
    }

   IEnumerator StartShake(float shakeDuration)
    {
        Shake(shakeDuration);
        yield return new WaitForSeconds(shakeDuration);
        Destroy(this.gameObject);
    }

    void Shake(float t)
    {
        isShaking = true;
        transform.GetChild(0).transform.DOShakePosition(t, .1f, 10, fadeOut: false);
    }

}
