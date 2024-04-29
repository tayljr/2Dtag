using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TaggerWonState : StateBase
{
	public GameObject taggerWinMenu;

	public override void Enter()
	{
		base.Enter();
		taggerWinMenu.SetActive(true);
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
