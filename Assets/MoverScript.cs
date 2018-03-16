using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour {

    public float speed;
    public List<GameObject> points;

    private GameObject target;
    private int targetN;

    private Transform passager;

    private void Awake()
    {
        target = points[0];
        targetN = 0;
    }

    private void FixedUpdate()
    {

        transform.Translate((target.transform.position - transform.position).normalized * speed * Time.deltaTime);

        if((target.transform.position - transform.position).magnitude <= 0.05)
        {
            RotateChangeTarget();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        passager = collision.gameObject.transform;
        passager.transform.SetParent(this.transform);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //passager.velocity = passager.velocity + this.GetComponent<Rigidbody2D>().velocity;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.DetachChildren();
    }

    private void RotateChangeTarget()
    {
        if(targetN == points.Count - 1)
        {
            targetN = 0;
        }
        else
        {
            targetN++;
        }

        target = points[targetN];

    }


}
