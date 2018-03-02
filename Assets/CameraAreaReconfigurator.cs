using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaReconfigurator : MonoBehaviour {

    public float clock;

    public float timeLimit;

    public float offset;
    public float height;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(clock >= timeLimit)
        {
            ShiftCamera();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>())
        {
            clock += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>())
        {
            clock =0;
        }
    }
    private void ShiftCamera()
    {
        CameraScript cam;

        cam = Camera.main.GetComponent<CameraScript>();

        cam.offset = offset;
        cam.height = height;


    }

}
