using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    public GameObject laserPrefab;

    private Animator animator;

    public int lenghtOfLaser;

    public float downTime;
    public float upTime;
    public float startDelay;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(FireRoutine(startDelay, upTime));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FireRoutine(float dowT, float upT)
    {
        yield return new WaitForSeconds(dowT);

        StartShooting();

        yield return new WaitForSeconds(upT);
        StopShooting();
        StartCoroutine(FireRoutine(downTime, upTime));

    }

    void StartShooting()
    {
        GameObject ray = null;

        animator.SetTrigger("Fire");

        for(int i = 0; i<lenghtOfLaser; i++)
        {
            if (i == 0)
            {
                ray = Instantiate(laserPrefab, transform);
                ray.transform.Translate(0.0f, 0.0f, 0.0f, transform);
            }
            else
            {
                GameObject rayB = Instantiate(laserPrefab, transform);
                rayB.transform.Translate((2.5f * i), 0.0f, 0.0f, ray.transform);
                ray = rayB;
            }
        }

    }

    void StopShooting()
    {
        animator.SetTrigger("Stop");
    }

}
