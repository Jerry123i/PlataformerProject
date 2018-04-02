using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [Range(10.0f, 20.0f)]
    public float impulseVel = 12.0f;

	
    void EnemyDie()
    {
        Destroy(gameObject);
    }

    private IEnumerator DeathAnimation()
    {
        BeforeDeath();

        yield return new WaitForSeconds(0.2f);

        EnemyDie();

    }

    public virtual void BeforeDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.localScale = new Vector3(transform.localScale.x, 0.15f, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        if(GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    public void PlayerKill(GameObject player)
    {
        ImpulsePlayer(player.GetComponent<Rigidbody2D>());
        StartCoroutine(DeathAnimation());

    }

    private void ImpulsePlayer(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(rb.velocity.x, impulseVel);
    }

}
