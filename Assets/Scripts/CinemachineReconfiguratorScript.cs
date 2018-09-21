using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider2D))]
public class CinemachineReconfiguratorScript : MonoBehaviour {

    public CinemachineVirtualCamera cinemachine;

    private GlobalCinemachineDirector director;

    private float clock;
    public float timeLimit;

    public bool triggerIsActive;
    public bool firstCamera;
    public bool staticCamera;

    private void Awake()
    {
        director = FindObjectOfType<GlobalCinemachineDirector>();        
    }

    private void Start()
    {
        if (firstCamera)
        {
            MoveCamera();
        }
    }

    private void Update()
    {
        if (!staticCamera)
        {
            if(clock>= timeLimit)
            {
                MoveCamera();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>())
        {
            Debug.Log("Trigger enter(" + name + ")");
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!staticCamera)
        {
            if (collision.GetComponent<PlayerScript>())
            {
                clock += Time.deltaTime;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!staticCamera)
        {
            if (collision.GetComponent<PlayerScript>())
            {
                clock = 0;
                triggerIsActive = true;
            }
        }
    }


    void MoveCamera()
    {
        Debug.Log("Camera move call(" + name+")");
        director.ActivateCamera(cinemachine);
    }

}
