using System.Collections;
using System.Text;
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

	public AudioManagerScript audioManager;

    private Coroutine loader;

	[Header("Return circle audio")]
	public AudioClip startCircle;
	public AudioClip finishCircle;
	private Coroutine circleCoroutine;
		
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if(save == null)
            {
                save = new SaveScript();
                save.saveInfo.GetLevel(1, 1).available = true;                
            }
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);        
    }

	public void QuickLoadStage(string stageName)
	{
		audioManager.SoundShift(false);
		SceneManager.LoadScene(stageName);
	}

    public void LoadStage(string stageName)
    {
		if(loader == null)
		{
			StartCoroutine(StageDelay(stageName));
		}
    }

	public IEnumerator StageDelay(string stageName)
	{
		yield return new WaitForSeconds(0.2f);
		if (save.saveInfo.GetLevel(SceneManager.GetActiveScene().name) != null)
		{
			save.saveInfo.GetLevel(SceneManager.GetActiveScene().name).completed = true;
		}

		save.UpdateSave();

		if (loader == null)
		{
			loader = StartCoroutine(LoadAsynch(stageName));
		}
	}

	private void SetLoadingAnimation(string name)
	{
		int world = 0;

		audioManager.SoundShift(false);

		foreach(char c in name)
		{
			if(int.TryParse(c.ToString(), out world))
			{
				blackScreen.GetComponentInChildren<Animator>().SetInteger("WorldKey", world);

				if(world != audioManager.currentWorld)
				{					
					audioManager.StartMusic(world);
				}

				return;
			}
		}

		blackScreen.GetComponentInChildren<Animator>().SetInteger("WorldKey", 0);

		if (world != audioManager.currentWorld)
		{
			audioManager.StartMusic(world);
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

		if (Input.GetButtonDown("Return"))
		{
			symbol.GetComponentInChildren<AudioSource>().clip = startCircle;
			symbol.GetComponentInChildren<AudioSource>().Play();
		}

		if (Input.GetButton("Return"))
        {

            returnCircle.GetComponent<Image>().fillAmount += Time.deltaTime * 0.7f;



            if(returnCircle.GetComponent<Image>().fillAmount >= 1)
            {

                //returnCircle.GetComponent<Image>().fillAmount = 0;
				if(circleCoroutine == null)
				{
					circleCoroutine = StartCoroutine(FinishedCircle());
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

	IEnumerator FinishedCircle()
	{
		symbol.GetComponentInChildren<AudioSource>().clip = finishCircle;
		symbol.GetComponentInChildren<AudioSource>().Play();


		yield return new WaitForSeconds(finishCircle.length-0.5f);

		symbol.SetActive(false);
		returnCircle.SetActive(false);


		if (SceneManager.GetActiveScene().name == "MainHub" || SceneManager.GetActiveScene().name == "IntroScene")
		{
			CloseGame();
		}
		else
		{
			LoadStage("MainHub");
		}

		circleCoroutine = null;

	}

	public void CloseGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsynch(string stageName)
    {
        AsyncOperation operation;

        blackScreen.SetActive(true);
		SetLoadingAnimation(stageName);
                
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
