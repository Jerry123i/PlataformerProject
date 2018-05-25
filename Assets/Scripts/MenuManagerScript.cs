using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenStageMenu(int n)
    {
        activeMenu = menusCanvas[n];        
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
