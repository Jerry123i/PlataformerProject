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
    
    public bool hubDoor;

    private void Awake()
    {

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
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private bool CheckLockCollect()
    {
        return GameObject.FindGameObjectsWithTag("Collectible").Length == 0;
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

                if (!(nextStageName == "MainHub"))
                {
                    StageManagerScript.save.saveInfo.GetLevel(nextStageName).available = true;
                }

                StageManagerScript.instance.LoadStage(nextStageName);

            }
            else
            {
                if (Input.GetAxisRaw("Vertical") >= 1 && MenuManagerScript.instance.ActiveMenu == null)
                {
                    MenuManagerScript.instance.OpenStageMenu(worldDoor);
                }
            }
        }
    }


}
