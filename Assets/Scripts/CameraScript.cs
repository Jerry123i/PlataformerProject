using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float moveStep;

    private GameObject player;

    public float offset;
    public float height;

    public bool followPlayer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        height = transform.position.y;
        
    }

    void Update () {

        if (followPlayer)
        {
            //transform.position = new Vector3(DivideByStep(player.GetComponent<Transform>().position.x + offset), DivideByStep(height), transform.position.z);
            transform.position = new Vector3(player.GetComponent<Transform>().position.x + offset, DivideByStep(height), transform.position.z);
        }
    }


    public IEnumerator MoveCamera(Vector3 targetPoint, float speed, bool followAfter)
    {
        do
        {
            Debug.Log("DoWhile");
            if (followAfter)
            {
                Vector3 adjustedtarget;

                adjustedtarget = new Vector3(player.transform.position.x + offset, targetPoint.y, targetPoint.z);
                transform.Translate((adjustedtarget - transform.position).normalized * speed * Time.deltaTime);

            }
            else
            {
                transform.Translate((targetPoint - transform.position).normalized * speed * Time.deltaTime);
            }
            
            transform.position = DivideByStepVector(transform.position);

            yield return null;
        } while ( !(followAfter && ((targetPoint.y - transform.position.y) <= 0.05)) && !(!followAfter && ((targetPoint - transform.position).magnitude <= 0.05)));

        
        height = DivideByStep(transform.position.y);
        followPlayer = followAfter;

    }

    private Vector3 DivideByStepVector(Vector3 v)
    {
        return new Vector3(DivideByStep(v.x), DivideByStep(v.y), DivideByStep(v.z));
    }



    private float DivideByStep(float val)
    {
        float x;

        x = Mathf.RoundToInt(val / moveStep);
        return (x * moveStep);
    }


}
