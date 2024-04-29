using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuOffState : StateBase
{
	public GameObject pauseMenu;
	
	public override void Enter()
	{
		base.Enter();
		pauseMenu.SetActive(false);
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
