using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MenuBackButtonScript : MenuButtonScript , ISelectHandler {

    private void OnEnable()
    {
        button.onClick.AddListener(delegate { MenuManagerScript.instance.CloseStageMenu(); });
    }

    public new void OnSelect(BaseEventData eventData)
    {
        Debug.Log(name + "OnSelect");
    }


}
