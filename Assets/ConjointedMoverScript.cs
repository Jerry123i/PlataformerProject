using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjointedMoverScript : MoverScript
{

    public MoverScript jointedMoverScript;

	// Use this for initialization
	void Start () {

        moverMode = jointedMoverScript.moverMode;
        working = jointedMoverScript.working;
		
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    //TODO FAZER ELE LIGAR QUANDO O OUTRO LIGA TAMBÉM



}
