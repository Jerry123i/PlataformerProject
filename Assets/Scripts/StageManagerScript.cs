using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour {

    public SaveScript save;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        save = new SaveScript();
        save.saveInfo.GetLevel(1, 1).available = true;
    }

    public void LoadStage(string stageName)
    {
        save.saveInfo.GetLevel(stageName).completed = true;
        SceneManager.LoadScene(stageName);
    }

    private void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            SceneManager.LoadScene("MainHub");
        }

        if (Input.GetKeyDown("1"))
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
