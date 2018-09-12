using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineReconfiguratorScript : MonoBehaviour {

    public GameObject focusPoint;

    public CinemachineVirtualCamera cinemachineFolow;
    public CinemachineVirtualCamera cinemachineStatic;


    private float clock;
    public float timeLimit;

    public bool active;

    private void Awake()
    {
        cinemachineStatic.Priority = 20;
        cinemachineFolow.Priority = 10;
    }

    private void Update()
    {
        if(clock>= timeLimit)
        {
            MoveCamera();
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


    void MoveCamera()
    {
        cinemachineFolow.Priority = 100;        
    }

}
