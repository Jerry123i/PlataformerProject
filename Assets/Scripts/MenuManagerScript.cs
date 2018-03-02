using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerScript : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenStageMenu()
    {

    }

    public void CloseStageMenu()
    {

    }

}
