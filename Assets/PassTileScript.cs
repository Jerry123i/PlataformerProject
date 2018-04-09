using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTileScript : MonoBehaviour {

    public bool playerInside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInside = false;
    }

}
