using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour {

    public WorldMenuScript[] menusCanvas;
    private WorldMenuScript activeMenu;
    private SaveScript save;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Debug.Log("Start");
        save = FindObjectOfType<StageManagerScript>().save;

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
        activeMenu = null;

        foreach(WorldMenuScript wms in menusCanvas)
        {
            if(wms.worldNumber == n)
            {
                activeMenu = wms;
            }
        }
        if(activeMenu == null)
        {
            Debug.Break();
        }


        activeMenu.gameObject.SetActive(true);

        for(int i = 0; i < activeMenu.list.Count; i++)
        {
            activeMenu.list[i].interactable = save.saveInfo.GetLevel(activeMenu.worldNumber, i + 1).available;
        }
    }

    public void CloseStageMenu()
    {
        activeMenu.gameObject.SetActive(false);
    }

}
