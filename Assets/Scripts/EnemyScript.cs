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
                Die();
            }
            else
            {
                collision.gameObject.GetComponent<PlayerScript>().Die();
            }
        }
    }

    private void ImpulsePlayer(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(rb.velocity.x, impulseVel);
    }

}
