using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float moveStep;

    private GameObject player;

    public float offsetMax;
    public float offsetMin;
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

            if(player.GetComponent<Transform>().position.x > transform.position.x + offsetMax)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.GetComponent<Transform>().position.x - offsetMax, transform.position.y, -10.0f), Time.deltaTime * 2.2f);
            }
            if(player.GetComponent<Transform>().position.x < transform.position.x + offsetMin)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.GetComponent<Transform>().position.x + offsetMin, transform.position.y, -10.0f), Time.deltaTime * 2.2f);
            }

            transform.position = DivideByStepVector(transform.position);

            //transform.position = new Vector3(DivideByStep(player.GetComponent<Transform>().position.x + offset), DivideByStep(height), transform.position.z);
            //transform.position = new Vector3(player.GetComponent<Transform>().position.x + offsetMax, DivideByStep(height), transform.position.z);
        }
    }


    public IEnumerator MoveCamera(Vector3 targetPoint, float speed, bool followAfter)
    {
        do
        {
            if (followAfter)
            {
                Vector3 adjustedtarget;

                adjustedtarget = new Vector3(player.transform.position.x, targetPoint.y, targetPoint.z);

                transform.position = Vector3.Lerp(transform.position, adjustedtarget, Time.deltaTime * 3.0f);

                //transform.Translate((adjustedtarget - transform.position).normalized * speed * Time.deltaTime);

            }
            else
            {
                transform.Translate((targetPoint - transform.position).normalized * speed * Time.deltaTime);
            }
            
            //transform.position = DivideByStepVector(transform.position);

            yield return null;
        } while ( !(followAfter && ((targetPoint.y - transform.position.y) <= 0.1)) && !(!followAfter && ((targetPoint - transform.position).magnitude <= 0.1)));

        if (!followAfter)
        {
            transform.position = targetPoint;
        }
        
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
