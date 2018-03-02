using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public string nextStageName;

    [HideInInspector]
    public bool open;

    public bool lockEnemy, lockCollect;
    
    private Animator animator;

    private StageManagerScript sm;

    private void Awake()
    {
        sm = FindObjectOfType<StageManagerScript>();
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
            animator.SetTrigger("DoorSwitch");
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (open && (collision.GetComponent<PlayerScript>()) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Open")))
        {
            sm.LoadStage(nextStageName);
        }
    }


}
