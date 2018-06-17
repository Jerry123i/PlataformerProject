using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManagerScript : MonoBehaviour {

    public WorldMenuScript[] menusCanvas;

    [SerializeField]
    public WorldMenuScript activeMenu;
    private SaveScript save;

    public static MenuManagerScript instance;

    public WorldMenuScript ActiveMenu { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

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


    public void OpenStageMenu(int n)
    {


        PlayerScript player;
        player = FindObjectOfType<PlayerScript>();
        FindObjectOfType<PlayerScript>().LockedMovement = true;

        activeMenu = null;


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

        if (activeMenu.list[0].interactable)
        {
            Debug.Log("Button1 selected");
            EventSystem.current.SetSelectedGameObject(activeMenu.list[0].gameObject);
            activeMenu.list[0].gameObject.GetComponent<MenuButtonScript>().OnSelect(new BaseEventData(EventSystem.current));
        }
        else
        {
            Debug.Log("Back selected");
            EventSystem.current.SetSelectedGameObject(activeMenu.list[activeMenu.list.Count - 1].gameObject);
            activeMenu.list[activeMenu.list.Count - 1].gameObject.GetComponent<MenuBackButtonScript>().OnSelect(new BaseEventData(EventSystem.current));
        }

    }

    public void CloseStageMenu()
    {

        PlayerScript player;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (player == null)
        {
            Debug.Break();
        }
        player.LockedMovement = false;

        ActiveMenu.gameObject.SetActive(false);
        ActiveMenu = null;

    }

}
