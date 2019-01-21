using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpCutscene : PlayerJump {

	public override void Update()
	{
		if (!player.LockedMovement)
		{

		}

		animator.SetBool("IsJumping", !IsOnFloor());
		GravitySetter();
		TerminalVelocity();
	}
}
