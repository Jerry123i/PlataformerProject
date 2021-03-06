﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float moveStep;

    private GameObject player;

    public float offsetMaxX;
    public float offsetMinX;
    private float YAnchor;

    public float offsetMaxY;
    public float offsetMinY;
    private float XAnchor;

    public bool followPlayerX;
    public bool followPlayerY;

    public float LerpT = 2.2f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        YAnchor = transform.position.y;
        
    }

    

    void Update () {

        if (followPlayerX)
        {

            if(player.GetComponent<Transform>().position.x > transform.position.x + offsetMaxX)
            {
                //transform.position = Vector3.Lerp(transform.position, new Vector3(player.GetComponent<Transform>().position.x - offsetMaxX, transform.position.y, -10.0f), Time.deltaTime * 2.2f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + (Mathf.Abs(offsetMaxX)), transform.position.y, -10.0f), Time.deltaTime * LerpT);
            }

            if(player.GetComponent<Transform>().position.x < transform.position.x + offsetMinX)
            {
                //transform.position = Vector3.Lerp(transform.position, new Vector3(player.GetComponent<Transform>().position.x + offsetMinX, transform.position.y, -10.0f), Time.deltaTime * 2.2f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - (Mathf.Abs(offsetMinX)), transform.position.y, -10.0f), Time.deltaTime * LerpT);
            }

            transform.position = DivideByStepVector(transform.position);
        }

        if (followPlayerY)
        {

            if (player.GetComponent<Transform>().position.y > transform.position.y + offsetMaxY)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x ,transform.position.y + (Mathf.Abs(offsetMaxY)), -10.0f), Time.deltaTime * LerpT);
            }
            if (player.GetComponent<Transform>().position.y < transform.position.y + offsetMinY)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - (Mathf.Abs(offsetMinY)) , -10.0f), Time.deltaTime * LerpT);
            }

            transform.position = DivideByStepVector(transform.position);
        }

    }


    public IEnumerator MoveCamera(Vector3 targetPoint, float speed, bool followAfterX, bool followAfterY)
    {

        Vector3 adjustedtarget;

        do
        {
            
            if (followAfterX)
            {

                adjustedtarget = new Vector3(player.transform.position.x, targetPoint.y, targetPoint.z);

                transform.position = Vector3.Lerp(transform.position, adjustedtarget, Time.deltaTime * 3.0f);

                //transform.Translate((adjustedtarget - transform.position).normalized * speed * Time.deltaTime);

            }

            if (followAfterY)
            {

                adjustedtarget = new Vector3(targetPoint.x, player.transform.position.y, targetPoint.z);

                transform.position = Vector3.Lerp(transform.position, adjustedtarget, Time.deltaTime * 3.0f);

                //transform.Translate((adjustedtarget - transform.position).normalized * speed * Time.deltaTime);

            }

            if (!followAfterX && !followAfterY)
            {
                transform.Translate((targetPoint - transform.position).normalized * speed * Time.deltaTime);
            }

            //transform.position = DivideByStepVector(transform.position);

            yield return null;

            

            if (followAfterX && followAfterY)
            {                
                break;
            }
            if (!followAfterX && !followAfterY)
            {
                if ((targetPoint - transform.position).magnitude <= 0.1 * speed / 5)
                {
                    break;
                }
            }
            

            else if (followAfterX)
            {
                if (Mathf.Abs(targetPoint.y - transform.position.y) <= 0.1 * speed / 5)
                {
                    break;
                }
            }
            else if (followAfterY)
            {
                if (Mathf.Abs(targetPoint.x - transform.position.x) <= 0.1 * speed / 5)
                {
                    break;
                }
            }


        } while (true);
        //} while (!(followAfterX && (Mathf.Abs(targetPoint.y - transform.position.y) <= 0.1 * speed )) && !(!followAfterX && ((targetPoint - transform.position).magnitude <= 0.1 * speed/5)));

        if (!followAfterX && !followAfterY)
        {
            transform.position = targetPoint;
        }

        
        YAnchor = DivideByStep(transform.position.y);
        XAnchor = DivideByStep(transform.position.x);
        followPlayerX = followAfterX;
        followPlayerY = followAfterY;

    }

    public IEnumerator MoveCamera(Vector3 targetPoint, float speed, bool followAfterX, bool followAfterY, float LerpT)
    {

        StartCoroutine(MoveCamera(targetPoint, speed, followAfterX, followAfterY));

        this.LerpT = LerpT;

        yield return null;

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
