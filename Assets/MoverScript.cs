using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoverMode {CYCLE, ROTATION, ONCE};
public enum MoverType {PLATAFORM, ENEMY};

public class MoverScript : MonoBehaviour {

    public MoverMode moverMode;
    public MoverType moverType;

    public float speed;

    
    public Vector3 rotationCenter;
    public bool reverse;
    public float radius;

    public List<Vector3> points;
    private Vector3 target;
    public int targetN;

    private Transform passager;

    public bool working = true;

    private void Awake()
    {
        target = points[targetN];
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

                case MoverMode.ONCE:
                    OnceMovement();
                    break;

                default:
                    break;
            }
        }

    }

    void OnceMovement()
    {
        transform.Translate((target - transform.position).normalized * speed * Time.deltaTime);

        if ((target - transform.position).magnitude <= 0.05 * speed / 4)
        {
            transform.position = target;

            if(!(targetN == points.Count - 1))
            {
                RotateChangeTarget();
            }
            else
            {
                working = false;
            }
            
        }
    }

    void CycleMovement()
    {
        transform.Translate((target - transform.position).normalized * speed * Time.deltaTime);

        if ((target - transform.position).magnitude <= 0.05 * speed/4)
        {
            transform.position = target;
            RotateChangeTarget();
        }
    }

    void RotationMovement()
    {
        Quaternion fixRotation;

        fixRotation = transform.rotation;

        if (reverse)
        {
            transform.RotateAround(rotationCenter, Vector3.forward, speed * Time.deltaTime* -1.0f * RotationKonstant());
        }
        else
        {
            transform.RotateAround(rotationCenter, Vector3.forward, speed * Time.deltaTime * RotationKonstant());
        }

        transform.rotation = fixRotation;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(moverType == MoverType.PLATAFORM)
        {
            working = true;

            passager = collision.gameObject.transform;
            passager.transform.SetParent(this.transform);
        }
        
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
        return (180/(Mathf.PI * r));
        
    }


}
