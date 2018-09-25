using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuHubGatekeeper : MonoBehaviour {
    
    private int gateToOpen;

    static int totalLevels = 3;

    //Gate 2 stuff
    public GameObject weakTile1;
    public GameObject weakTile2;
    public GameObject gate2Camera;

    //Gate 3 stuff
    public GameObject laserBarrier;
    public GameObject gate3Camera;

    public GameObject cameraFollow;

    public List<GameObject> gateTriggers;

    private GlobalCinemachineDirector director;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        director = FindObjectOfType<GlobalCinemachineDirector>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoad MenuHUBGate");
                
        UpdateWorldInfo();
        OpenAllValidGates();
        ReadyNextGate();

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void UpdateWorldInfo()
    {
        for(int i = 1; i<=totalLevels; i++)
        {
            
            if(StageManagerScript.save.saveInfo.CheckWorldCompletion(i))
            {
                StageManagerScript.save.saveInfo.gates[i].completed = true;
            }
            
            if(StageManagerScript.save.saveInfo.gates[i].completed == true && i != totalLevels)
            {
                if(StageManagerScript.save.saveInfo.gates[i+1].opened == false)
                {
                    gateToOpen = i + 1;
                    Debug.Log("Open gate " + gateToOpen.ToString());
                }
            }
        }
    }

    private void OpenAllValidGates()
    {
        if (StageManagerScript.save.saveInfo.gates[2].opened)
        {
            Destroy(weakTile1);
            Destroy(weakTile2);
        }

        if (StageManagerScript.save.saveInfo.gates[3].opened)
        {
            Destroy(laserBarrier);
        }

    }

    private void ReadyNextGate()
    { 
        foreach(GameObject go in gateTriggers)
        {
            if(go != null)
            {
                go.SetActive(false);
            }
        }

        if(gateToOpen != 0)
        {
            gateTriggers[gateToOpen].SetActive(true);
        }
        gateToOpen = 0;
    }

    IEnumerator OpenGate2()
    {

        Vector3 cameraPreviousPosition;

        FindObjectOfType<PlayerScript>().LockedMovement = true;
        FindObjectOfType<PlayerScript>().GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //cameraFollow.GetComponent<CameraScript>().enabled = false;
        cameraPreviousPosition = cameraFollow.transform.position;
        director.ActivateCamera(gate2Camera);
        gate2Camera.transform.DOMove(new Vector3(weakTile1.transform.position.x, weakTile1.transform.position.y, Camera.main.transform.position.z), 0.6f);

        yield return new WaitForSeconds(1.1f);

        weakTile1.GetComponent<WeakTileScript>().enabled = true;
        weakTile1.GetComponent<WeakTileScript>().StartCoroutine(weakTile1.GetComponent<WeakTileScript>().StartShake(1f));
        weakTile2.GetComponent<WeakTileScript>().enabled = true;
        weakTile2.GetComponent<WeakTileScript>().StartCoroutine(weakTile2.GetComponent<WeakTileScript>().StartShake(1.2f));

        yield return new WaitForSeconds(1.7f);

        gate2Camera.transform.DOMove(cameraPreviousPosition, 0.6f);
        //cameraFollow.GetComponent<CameraScript>().enabled = true;
        director.ActivateCamera(cameraFollow);
        FindObjectOfType<PlayerScript>().LockedMovement = false;

        StageManagerScript.save.saveInfo.GetLevel(2, 1).available = true;

    }

    IEnumerator OpenGate3()
    {
        Vector3 cameraPreviousPosition;

        FindObjectOfType<PlayerScript>().LockedMovement = true;
        FindObjectOfType<PlayerScript>().GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //cameraFollow.GetComponent<CameraScript>().enabled = false;
        cameraPreviousPosition = cameraFollow.transform.position;
        director.ActivateCamera(gate3Camera);
        gate3Camera.transform.DOMove(new Vector3(laserBarrier.transform.position.x, laserBarrier.transform.position.y, Camera.main.transform.position.z - 2.5f), 0.6f);

        yield return new WaitForSeconds(1.1f);

        laserBarrier.GetComponent<LaserScript>().StopShooting();

        yield return new WaitForSeconds(0.8f);

        cameraFollow.transform.DOMove(cameraPreviousPosition, 0.6f);
        //cameraFollow.GetComponent<CameraScript>().enabled = true;
        director.ActivateCamera(cameraFollow);
        FindObjectOfType<PlayerScript>().LockedMovement = false;

        StageManagerScript.save.saveInfo.GetLevel(3, 1).available = true;


    }

    public void StartAnimations(int world)
    {
        switch (world)
        {
            case 2:
                StartCoroutine(OpenGate2());    
                break;
            case 3:
                StartCoroutine(OpenGate3());
                break;
        }

        StageManagerScript.save.saveInfo.gates[world].opened = true;
        StageManagerScript.save.UpdateSave();
        gateTriggers[world].SetActive(false);
        gateToOpen = 0;

    }

}

[System.Serializable]
public class WorldGate{

    public bool completed;
    public bool opened;

}
