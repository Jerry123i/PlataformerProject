using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManagerScript : MonoBehaviour {

    public static SaveScript save;

    public static StageManagerScript instance;

    public Sprite spriteReturn;
    public Sprite spriteExit;

    public GameObject symbol;
    public GameObject returnCircle;

    public GameObject blackScreen;

    private Coroutine loader;
    
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
        //Debug.Log(SceneManager.GetActiveScene());
        if(save.saveInfo.GetLevel(SceneManager.GetActiveScene().name) != null)
        {
            save.saveInfo.GetLevel(SceneManager.GetActiveScene().name).completed = true;
        }

        save.UpdateSave();
        //SceneManager.LoadScene(stageName);

        if(loader == null)
        {
            loader = StartCoroutine(LoadAsynch(stageName));
        }

    }

    private void Update()
    {

        if (Input.GetButtonDown("Return"))
        {
            if(SceneManager.GetActiveScene().name == "MainHub" || SceneManager.GetActiveScene().name == "IntroScene")
            {
                symbol.GetComponent<Image>().sprite = spriteExit;
            }
            else
            {
                symbol.GetComponent<Image>().sprite = spriteReturn;
            }

            returnCircle.SetActive(true);
            symbol.SetActive(true);
        }

        if (Input.GetButton("Return"))
        {

            returnCircle.GetComponent<Image>().fillAmount += Time.deltaTime * 0.7f;

            if(returnCircle.GetComponent<Image>().fillAmount >= 1)
            {
                returnCircle.GetComponent<Image>().fillAmount = 0;
                symbol.SetActive(false);
                returnCircle.SetActive(false);

                if (SceneManager.GetActiveScene().name == "MainHub" || SceneManager.GetActiveScene().name == "IntroScene")
                {
                    CloseGame();
                }
                else
                {
                    SceneManager.LoadScene("MainHub");
                }

            }

        }

        if (Input.GetButtonUp("Return"))
        {
            returnCircle.GetComponent<Image>().fillAmount = 0;
            symbol.SetActive(false);
            returnCircle.SetActive(false);
        }

    }

    public void CloseGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsynch(string stageName)
    {
        AsyncOperation operation;

        blackScreen.SetActive(true);
                
        yield return new WaitForSeconds(1.1f);

        operation = SceneManager.LoadSceneAsync(stageName);

        while (!operation.isDone)
        {
            yield return null;
        }

        blackScreen.SetActive(false);
        loader = null;
    }


}
