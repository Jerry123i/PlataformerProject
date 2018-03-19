using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [Range(10.0f, 20.0f)]
    public float impulseVel = 12.0f;

	
    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>())
        {
            if (collision.transform.position.y > (transform.position.y + (transform.localScale.y / 2)))
            {
                ImpulsePlayer(collision.gameObject.GetComponent<Rigidbody2D>());
                StartCoroutine(DeathAnimation());
            }
            else
            {
                collision.gameObject.GetComponent<PlayerScript>().Die();
            }
        }
    }

    private IEnumerator DeathAnimation()
    {
        BeforeDeath();

        yield return new WaitForSeconds(0.2f);

        Die();

    }

    public virtual void BeforeDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.localScale = new Vector3(transform.localScale.x, 0.15f, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    private void ImpulsePlayer(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(rb.velocity.x, impulseVel);
    }

}
