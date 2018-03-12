using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverScript : MonoBehaviour {

    public float speed;
    public List<GameObject> points;

    private GameObject target;
    private int targetN;

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

    private void RotateChangeTarget()
    {
        Debug.Log("Click");
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
