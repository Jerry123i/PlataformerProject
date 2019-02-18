using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeakTileScript : MonoBehaviour {

    protected bool isShaking;
    float time;
    public float maxTime = 0.7f;
    new public bool enabled = true;

	public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isShaking && enabled)
        {

            foreach (var contact in collision.contacts)
            {

                if (contact.point.y > transform.position.y)
                {
                    StartCoroutine(StartShake(maxTime));
                    return;
                }
            }

            
        }
    }

   public virtual IEnumerator StartShake(float shakeDuration)
    {
        Shake(shakeDuration);
		audioSource.Play();
        yield return new WaitForSeconds(shakeDuration);
        Fall();
        yield return new WaitForSeconds(0.95f);
        Destroy(this.gameObject);
    }

    protected virtual void Fall()
    {
        transform.DOLocalMoveY((transform.position.y - 15.0f), 1.0f);
        this.GetComponent<Collider2D>().enabled = false;
    }

    protected virtual void Shake(float t)
    {
        isShaking = true;
        transform.GetChild(0).transform.DOShakePosition(t, .1f, 10, fadeOut: false);
    }

}
