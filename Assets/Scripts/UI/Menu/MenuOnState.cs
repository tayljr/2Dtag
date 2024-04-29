using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuOnState : StateBase
{
	public GameObject pauseMenu;
    
    public override void Enter()
    {
	    base.Enter();
	    pauseMenu.SetActive(true);
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
