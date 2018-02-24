using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	
    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            if (collision.transform.position.y > (transform.position.y + (transform.localScale.y / 2)))
            {
                ImpulsePlayer(collision.gameObject.GetComponent<Rigidbody2D>());
                Die();
            }
            else
            {
                Debug.Log("Dmg Player");
            }
        }
    }

    private void ImpulsePlayer(Rigidbody2D rb)
    {
        rb.velocity += Vector2.up * 6.0f;
    }

}
