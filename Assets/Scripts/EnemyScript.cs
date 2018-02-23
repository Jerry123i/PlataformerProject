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
                Die();
            }
            else
            {
                Debug.Log("Dmg Player");
            }
        }
    }

}
