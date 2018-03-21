using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoverMode {CYCLE, ROTATION};

public class MoverScript : MonoBehaviour {

    public MoverMode moverMode;

    public float speed;
    public List<Vector3> points;
    public Vector3 rotationCenter;
    public bool reverse;

    private Vector3 target;
    public int targetN;

    private Transform passager;

    public bool working = true;

    private void Awake()
    {
        target = points[0];
    }

    private void FixedUpdate()
    {
        if (working)
        {
            switch (moverMode)
            {
                case MoverMode.CYCLE:
                    CycleMovement();
                    break;

                case MoverMode.ROTATION:
                    RotationMovement();
                    break;

                default:
                    break;
            }
        }

    }

    void CycleMovement()
    {
        transform.Translate((target - transform.position).normalized * speed * Time.deltaTime);

        if ((target - transform.position).magnitude <= 0.1)
        {
            transform.position = target;
            RotateChangeTarget();
        }
    }

    void RotationMovement()
    {
        if (reverse)
        {
            transform.RotateAround(rotationCenter, Vector3.forward, speed * Time.deltaTime* -1.0f * RotationKonstant());
        }
        else
        {
            transform.RotateAround(rotationCenter, Vector3.forward, speed * Time.deltaTime * RotationKonstant());
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        working = true;

        passager = collision.gameObject.transform;
        passager.transform.SetParent(this.transform);
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

    private float RotationKonstant()
    {
        float r;

        r = (this.transform.position - rotationCenter).magnitude;
        Debug.Log("K = " + (((Mathf.PI * r) / 180.0f)).ToString());

        return (180/(Mathf.PI * r));
        
    }


}
