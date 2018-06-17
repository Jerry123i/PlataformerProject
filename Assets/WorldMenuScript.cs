using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenuScript : MonoBehaviour {

    public List<Button> list;
    public int worldNumber;
<<<<<<< HEAD
=======
    
    private void OnEnable()
    {
        FindObjectOfType<PlayerScript>().LockedMovement = true;
        EventSystem.current.SetSelectedGameObject(list[0].gameObject);
    }

    private void OnDisable()
    {
        FindObjectOfType<PlayerScript>().LockedMovement = false;
    }
>>>>>>> 61d29e13d2cf22f709d802c0fc4560c101271c65

}
