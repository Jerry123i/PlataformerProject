using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }

    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("Level2_1");
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("Level2_2");
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("Level2_3");
        }
        if (Input.GetKeyDown("4"))
        {
            SceneManager.LoadScene("Level2_4");
        }
        if (Input.GetKeyDown("5"))
        {
            SceneManager.LoadScene("Level2_5");
        }
        if (Input.GetKeyDown("6"))
        {
            SceneManager.LoadScene("Level2_6");
        }
        if (Input.GetKeyDown("7"))
        {
            SceneManager.LoadScene("Level2_7");
        }
    }

}
