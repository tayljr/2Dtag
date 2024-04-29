using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OptionsOffState : StateBase
{
	public GameObject optionsMenu;

	public override void Enter()
	{
		base.Enter();
		optionsMenu.SetActive(false);
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
