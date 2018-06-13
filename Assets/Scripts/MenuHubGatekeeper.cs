using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuHubGatekeeper : MonoBehaviour {

    private List<WorldGate> gates;
    private int gateToOpen;

    static int totalLevels = 2;

    public GameObject weakTile1;
    public GameObject weakTile2;
    public GameObject movingPlataformWorld2;

    private GameObject cameraGO;

    private void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            StartCoroutine(OpenGate2());
        }
    }

    private void Awake()
    {
        gates = new List<WorldGate>();

        for (int i = 0; i<totalLevels; i++)
        {
            gates.Add(new WorldGate());
        }

        gates[0].opened = true;
        cameraGO = Camera.main.gameObject;
        DontDestroyOnLoad(this);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cameraGO = Camera.main.gameObject;
        UpdateWorldInfo();
    }

    private void UpdateWorldInfo()
    {
        for(int i = 0; i<totalLevels; i++)
        {
            if(StageManagerScript.save.saveInfo.CheckWorldCompletion(i + 1))
            {
                gates[i].completed = true;

                if(gates[i].completed == true && i!= totalLevels-1)
                {
                    if(gates[i+1].opened == false)
                    {
                        gateToOpen = i + 1;
                    }
                }

            }
        }
    }


    IEnumerator OpenGate2()
    {

        Vector3 cameraPreviousPosition;

        FindObjectOfType<PlayerScript>().LockedMovement = true;
        FindObjectOfType<PlayerScript>().GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        cameraGO.GetComponent<CameraScript>().enabled = false;
        cameraPreviousPosition = cameraGO.transform.position;
        cameraGO.transform.DOMove(new Vector3(weakTile1.transform.position.x, weakTile1.transform.position.y, Camera.main.transform.position.z), 0.6f);

        yield return new WaitForSeconds(1.1f);

        weakTile1.GetComponent<WeakTileScript>().enabled = true;
        weakTile1.GetComponent<WeakTileScript>().StartCoroutine(weakTile1.GetComponent<WeakTileScript>().StartShake(1f));
        weakTile2.GetComponent<WeakTileScript>().enabled = true;
        weakTile2.GetComponent<WeakTileScript>().StartCoroutine(weakTile2.GetComponent<WeakTileScript>().StartShake(1.2f));

        yield return new WaitForSeconds(1.7f);

        cameraGO.transform.DOMove(cameraPreviousPosition, 0.6f);
        cameraGO.GetComponent<CameraScript>().enabled = true;
        FindObjectOfType<PlayerScript>().LockedMovement = false;

        StageManagerScript.save.saveInfo.GetLevel(2, 1).available = true;

    }

    public void StartAnimations(int world)
    {
        switch (world)
        {
            case 2:
                StartCoroutine(OpenGate2());
                break;
        }
    }

}

public class WorldGate{

    public bool completed;
    public bool opened;

}
