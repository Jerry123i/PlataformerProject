using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GlobalCinemachineDirector : MonoBehaviour {

    private List<CinemachineVirtualCamera> cameras;

    private void Awake()
    {
        cameras = new List<CinemachineVirtualCamera>();

        foreach(CinemachineVirtualCamera vc in FindObjectsOfType<CinemachineVirtualCamera>())
        {
            cameras.Add(vc);
            vc.gameObject.SetActive(false);
        }
    }

    private void DisableAll()
    {
        foreach (CinemachineVirtualCamera vc in FindObjectsOfType<CinemachineVirtualCamera>())
        {
            vc.gameObject.SetActive(false);
        }
    }

    public void ActivateCamera(CinemachineVirtualCamera camera)
    {
        DisableAll();
        camera.gameObject.SetActive(true);
    }

    public void ActivateCamera(GameObject camera)
    {
        DisableAll();
        camera.SetActive(true);
        Debug.Log("Tentativa de ativar: " + camera.ToString());
    }

}
