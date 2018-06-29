using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeakTileOffScript : MonoBehaviour
{

    protected bool isShaking;    
    new public bool enabled = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isShaking && enabled)
        {

            foreach (var contact in collision.contacts)
            {

                if (contact.point.y > transform.position.y)
                {
                    Shake();
                    return;
                }
            }


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && enabled && isShaking)
        {
            StartCoroutine(FallAndDestroy());
        }
    }

    public virtual IEnumerator FallAndDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Fall();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    protected virtual void Fall()
    {
        transform.DOLocalMoveY((transform.position.y - 15.0f), 3.0f);
        this.GetComponent<Collider2D>().enabled = false;
    }

    protected virtual void Shake()
    {
        isShaking = true;
        transform.GetChild(0).transform.DOShakePosition(100.0f, .08f, 5, fadeOut: false);
    }

}

