using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour {

    public WorldMenuScript[] menusCanvas;
    private SaveScript save;

    public WorldMenuScript ActiveMenu { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Debug.Log("Start");
        save = StageManagerScript.save;

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        menusCanvas = Resources.FindObjectsOfTypeAll<WorldMenuScript>();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenStageMenu(int n)
    {

        foreach(WorldMenuScript wms in menusCanvas)
        {
            if(wms.worldNumber == n)
            {
                ActiveMenu = wms;
            }
        }
        if(ActiveMenu == null)
        {
            Debug.Break();
        }

        ActiveMenu.gameObject.SetActive(true);

        for(int i = 0; i < ActiveMenu.list.Count; i++)
        {
            ActiveMenu.list[i].interactable = save.saveInfo.GetLevel(ActiveMenu.worldNumber, i + 1).available;
        }
    }

    public void CloseStageMenu()
    {
        ActiveMenu.gameObject.SetActive(false);
        ActiveMenu = null;
    }

}
