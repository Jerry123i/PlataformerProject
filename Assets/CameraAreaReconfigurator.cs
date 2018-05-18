using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaReconfigurator : MonoBehaviour {

    private float clock;

    public float timeLimit;

    public float offsetMaxX;
    public float offsetMinX;
    private float YAnchor;

    public float offsetMaxY;
    public float offsetMinY;
    private float XAnchor;

    public float cameraSpeed;
    public bool followX;
    public bool followY;

    public float LerpT = 2.2f;

    public bool active;
    
    private void Awake()
    {
        
    }

    private void Update()
    {
        if(clock >= timeLimit && active)
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
            clock = 0;
            active = true;
        }
    }
    private void ShiftCamera()
    {
        active = false;
        CameraScript cam;
        cam = Camera.main.GetComponent<CameraScript>();

        cam.followPlayerX = false;
        cam.offsetMaxX = offsetMaxX;
        cam.offsetMinX = offsetMinX;

        cam.followPlayerY = false;
        cam.offsetMaxY = offsetMaxY;
        cam.offsetMinY = offsetMinY;

        cam.StartCoroutine(cam.MoveCamera(transform.position, cameraSpeed, followX, followY, LerpT));

    }

}
