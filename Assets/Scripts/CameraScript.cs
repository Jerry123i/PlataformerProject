using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float moveStep;

    private GameObject player;

    public float offset;
    public float height;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        height = transform.position.y;

        Debug.Log(Mathf.RoundToInt(height / moveStep));

    }

    void Update () {

        transform.position = new Vector3(DivideByStep(player.GetComponent<Transform>().position.x + offset), DivideByStep(height), transform.position.z);

	}

    private float DivideByStep(float val)
    {
        float x;

        x = Mathf.RoundToInt(val / moveStep);        
        return (x)*moveStep;
    }
}
