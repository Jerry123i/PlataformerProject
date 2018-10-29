﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public string nextStageName;
    public int worldDoor;

    [HideInInspector]
    public bool open;

    public bool lockEnemy, lockCollect;
    
    private Animator animator;
    
    protected virtual void Awake()
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (open && (collision.GetComponent<PlayerScript>()))
        {
            animator.SetTrigger("DoorSwitchOpen");
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (open && (collision.GetComponent<PlayerScript>()))
        {            
            animator.SetTrigger("DoorSwitchClose");
        }
    }
        

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (open && (collision.GetComponent<PlayerScript>()) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Open")))
        {
         
            
            if (!(nextStageName == "MainHub"))
            {
                StageManagerScript.save.saveInfo.GetLevel(nextStageName).available = true;
            }

            StageManagerScript.instance.LoadStage(nextStageName);

        }
    }


}
