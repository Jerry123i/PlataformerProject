using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldMenuScript : MonoBehaviour {

    public List<Button> list;
    public int worldNumber;
    
    private void OnEnable()
    {
        FindObjectOfType<PlayerScript>().LockedMovement = true;
        EventSystem.current.SetSelectedGameObject(list[0].gameObject);
    }

    private void OnDisable()
    {
        FindObjectOfType<PlayerScript>().LockedMovement = false;
    }

}
