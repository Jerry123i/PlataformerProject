using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour {

    MenuHubGatekeeper parent;
    public int world;

    private void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<MenuHubGatekeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent.StartAnimations(world);
    }

}
