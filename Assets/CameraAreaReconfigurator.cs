using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaReconfigurator : MonoBehaviour {

    private float clock;

    public float timeLimit;

    public float offsetMax;
    public float offsetMin;
    private float height;

    public float cameraSpeed;
    public bool follow;

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

        cam.followPlayer = false;
        cam.offsetMax = offsetMax;
        cam.offsetMin = offsetMin;

        cam.StartCoroutine(cam.MoveCamera(transform.position, cameraSpeed, follow));

        Debug.Log("ShiftCamera[" + this.gameObject.name + "]");

    }

}
