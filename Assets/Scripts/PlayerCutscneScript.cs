﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Modes { FALLING, WALKING, ENDING};

public class PlayerCutscneScript : PlayerScript {

	private Modes mode = Modes.FALLING;

	public Modes Mode
	{
		get
		{
			return mode;
		}

		set
		{
			mode = value;
		}
	}

	void Update () {
		
		switch (Mode)
		{
			case Modes.ENDING:
				break;
			case Modes.FALLING:
				break;
			case Modes.WALKING:
				idleClock = 0;
				if (rb.velocity.x < speedCap)
				{
					//rb.velocity += currentSpeed * Time.deltaTime * Vector2.right;
				}
				break;
		}
		
	}

	protected override void PlayerMove()
	{

	}

	public override IEnumerator StartFallAnimation()
	{
		animator.SetTrigger("StartFallAnimation");
		LockedMovement = true;
		yield return null;
		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);

		yield return new WaitForSeconds(4.5f);

		LockedMovement = false;
		Mode = Modes.WALKING;
	}
}
