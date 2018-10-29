using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoorScript : DoorScript
{

    public GameObject upArrow;

    protected override void Awake()
    {
        base.Awake();
        upArrow.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        upArrow.SetActive(true);

    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetAxisRaw("Vertical") >= 1 && MenuManagerScript.instance.ActiveMenu == null)
        {
            MenuManagerScript.instance.OpenStageMenu(worldDoor);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        upArrow.SetActive(false);

    }


}
