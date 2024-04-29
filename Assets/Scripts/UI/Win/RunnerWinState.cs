using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RunnerWinState : StateBase
{
	public GameObject runnerWinMenu;

	public override void Enter()
	{
		base.Enter();
		runnerWinMenu.SetActive(true);
	}

	public override void Execute()
	{
		base.Execute();
	}

	public override void Exit()
	{
		base.Exit();
	}
	
}
