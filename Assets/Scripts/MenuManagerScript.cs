using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManagerScript : MonoBehaviour {

    public WorldMenuScript[] menusCanvas;

    [SerializeField]
    private WorldMenuScript activeMenu;
    //private SaveScript save;

    public static MenuManagerScript instance;

    public WorldMenuScript ActiveMenu { get { return activeMenu; } private set { activeMenu = value; } }


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

    /*private void Start()
    {
        Debug.Log("Start");

        save = StageManagerScript.save;

    }*/

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

        ActiveMenu = null;


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
            ActiveMenu.list[i].interactable = StageManagerScript.save.saveInfo.GetLevel(ActiveMenu.worldNumber, i + 1).available;
        }

        //Debug.Log("activeMenu:" + activeMenu.ToString());
        //Debug.Log("activeMen")

        if (ActiveMenu.list[0].interactable)
        {
            Debug.Log("Button1 selected");
            EventSystem.current.SetSelectedGameObject(ActiveMenu.list[0].gameObject);
            ActiveMenu.list[0].gameObject.GetComponent<MenuButtonScript>().OnSelect(new BaseEventData(EventSystem.current));
        }
        else
        {
            Debug.Log("Back selected");
            EventSystem.current.SetSelectedGameObject(ActiveMenu.list[ActiveMenu.list.Count - 1].gameObject);
            ActiveMenu.list[activeMenu.list.Count - 1].gameObject.GetComponent<MenuBackButtonScript>().OnSelect(new BaseEventData(EventSystem.current));
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
