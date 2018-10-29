using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour {

    public static SaveScript save;

    public static StageManagerScript instance;
    
    private void Awake()
    {

        Debug.Log("Stage Manager(Awake)");

        if (instance == null)
        {
            instance = this;
            if(save == null)
            {
                save = new SaveScript();
                save.saveInfo.GetLevel(1, 1).available = true;                
                //SceneManager.LoadScene("IntroScene");
            }
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);        
    }

    public void LoadStage(string stageName)
    {
        Debug.Log(SceneManager.GetActiveScene());
        if(save.saveInfo.GetLevel(SceneManager.GetActiveScene().name) != null)
        {
            save.saveInfo.GetLevel(SceneManager.GetActiveScene().name).completed = true;
        }

        save.UpdateSave();
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
            SceneManager.LoadScene("Level3_1");
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("Level3_2");
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("Level3_3");
        }
        if (Input.GetKeyDown("4"))
        {
            SceneManager.LoadScene("Level3_4");
        }
        if (Input.GetKeyDown("5"))
        {
            SceneManager.LoadScene("Level3_5");
        }
        if (Input.GetKeyDown("6"))
        {
            SceneManager.LoadScene("Level3_6");
        }
        if (Input.GetKeyDown("7"))
        {
            SceneManager.LoadScene("Level3_7");
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }


}
