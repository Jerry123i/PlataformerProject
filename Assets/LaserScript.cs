using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    public GameObject laserPrefab;

    private Animator animator;
    private Collider2D trigger;

    public int lenghtOfLaser;
    private GameObject[] lasers;

    public float downTime;
    public float upTime;
    public float startDelay;

    private float colliderTurnOnDelay = 0.34f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        trigger = GetComponent<Collider2D>();

        GameObject ray = null;

        lasers = new GameObject[lenghtOfLaser];

        for (int i = 0; i < lenghtOfLaser; i++)
        {
            if (i == 0)
            {
                ray = Instantiate(laserPrefab, transform);
                ray.transform.Translate(2.5f, 0.0f, 0.0f, transform);
            }
            else
            {
                GameObject rayB = Instantiate(laserPrefab, transform);
                rayB.transform.Translate((2.5f * i), 0.0f, 0.0f, ray.transform);
                ray = rayB;
            }

            lasers[i] = ray;

        }

        if(upTime <= 0.4f)
        {
            colliderTurnOnDelay = upTime * (4/7);
        }

    }

   
    void Start () {
        StartCoroutine(FireRoutine(startDelay, upTime));
	}
	
    IEnumerator FireRoutine(float dowT, float upT)
    {
        yield return new WaitForSeconds(dowT);

        StartShooting();

        yield return new WaitForSeconds(colliderTurnOnDelay);

        ActivateColliders();
        
        yield return new WaitForSeconds(upT - colliderTurnOnDelay);
        StopShooting();
        StartCoroutine(FireRoutine(downTime, upTime));

    }

    void StartShooting()
    {
        animator.SetTrigger("Fire");

        foreach (GameObject ray in lasers)
        {
            ray.GetComponent<Animator>().SetTrigger("Fire");
        }

    }

    void ActivateColliders()
    {
        trigger.enabled = true;

        foreach(GameObject ray in lasers)
        {
            ray.GetComponent<Collider2D>().enabled = true;
        }

    }

    void StopShooting()
    {
        animator.SetTrigger("Stop");
        trigger.enabled = false;

        foreach(GameObject ray in lasers)
        {
            ray.GetComponent<Animator>().SetTrigger("Stop");
            ray.GetComponent<Collider2D>().enabled = false;
        }

    }

}
