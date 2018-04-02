using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour {
    
    public void LoadStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }

    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("Level1_1");
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("Level1_2");
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("Level1_3");
        }
        if (Input.GetKeyDown("4"))
        {
            SceneManager.LoadScene("Level1_4");
        }
        if (Input.GetKeyDown("5"))
        {
            SceneManager.LoadScene("Level1_5");
        }
        if (Input.GetKeyDown("6"))
        {
            SceneManager.LoadScene("Level1_6");
        }
        if (Input.GetKeyDown("7"))
        {
            SceneManager.LoadScene("Level1_7");
        }
    }

}
