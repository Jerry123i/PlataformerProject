using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public string nextStageName;
    public int worldDoor;

    [HideInInspector]
    public bool open;

    public bool lockEnemy, lockCollect;
    
    private Animator animator;

    private StageManagerScript sm;
    private MenuManagerScript mm;

    public bool hubDoor;

    private void Awake()
    {
        sm = FindObjectOfType<StageManagerScript>();

        if (hubDoor)
        {
            mm = FindObjectOfType<MenuManagerScript>();
        }
        else
        {
            mm = null;
        }

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (VerifyUnlock())
        {
            LockUnlock(true);
        }
    }

    private void LockUnlock(bool x)
    {        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (open && (collision.GetComponent<PlayerScript>()))
        {
            animator.SetTrigger("DoorSwitchOpen");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (open && (collision.GetComponent<PlayerScript>()))
        {            
            animator.SetTrigger("DoorSwitchClose");
        }
    }
        

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (open && (collision.GetComponent<PlayerScript>()) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Open")))
        {
         
            if (!hubDoor)
            {
                sm.save.saveInfo.GetLevel(nextStageName).available = true;
                sm.LoadStage(nextStageName);
            }
            else
            {
                if (Input.GetButtonDown("Vertical"))
                {
                    mm.OpenStageMenu(worldDoor);
                }
            }
        }
    }


}
