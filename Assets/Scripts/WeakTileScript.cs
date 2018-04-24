using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeakTileScript : MonoBehaviour {

    bool isShaking;
    float time;
    public float maxTime = 0.7f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isShaking)
        {
            StartCoroutine(StartShake(maxTime));
        }
    }

   IEnumerator StartShake(float shakeDuration)
    {
        Shake(shakeDuration);
        yield return new WaitForSeconds(shakeDuration);
        transform.DOLocalMoveY(-20.0f, 1.0f);
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

    void Shake(float t)
    {
        isShaking = true;
        transform.GetChild(0).transform.DOShakePosition(t, .1f, 10, fadeOut: false);
    }

}
