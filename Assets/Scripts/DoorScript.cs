using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool open;

    public bool lockEnemy, lockCollect;

    public GameObject lightObject;
    public Color colorOpen, colorClosed;

    private void Update()
    {
        if (VerifyUnlock())
        {
            LockUnlock(true);
        }
    }

    private void LockUnlock(bool x)
    {
        if (x)
        {
            lightObject.GetComponent<SpriteRenderer>().color = colorOpen;
        }
        else
        {
            lightObject.GetComponent<SpriteRenderer>().color = colorClosed;
        }

        open = x;
    }

    private bool VerifyUnlock()
    {
        bool lE = false, lC = false;

        if (lockEnemy)
        {
            lE = CheckLockEnemy();
        }
        else
        {
            lE = true;
        }

        if (lockCollect)
        {
            lC = CheckLockCollect();
        }
        else
        {
            lC = true;
        }

        return (lE && lC);
    }

    private bool CheckLockEnemy()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckLockCollect()
    {
        if (GameObject.FindGameObjectsWithTag("Collectible").Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
