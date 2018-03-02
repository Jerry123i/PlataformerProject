using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private GameObject player;

    public float offset;
    public float height;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        height = transform.position.y;
    }

    void Update () {

        transform.position = new Vector3(player.GetComponent<Transform>().position.x + offset, height, transform.position.z);

	}
}
