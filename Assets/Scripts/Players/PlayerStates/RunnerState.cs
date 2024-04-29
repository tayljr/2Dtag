using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunnerState : StateBase
{
	public Collider2D weapon;
    private bool active = true;
    
    public override void Enter()
    {
      	base.Enter();
        active = true;
    }

    public override void Execute()
    {
      base.Execute();
    }

    public override void Exit()
    {
      base.Exit();
      active = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        weapon = FindObjectOfType<Weapon>().gameObject.GetComponent<Collider2D>();
        if (other == weapon && active)
        {
            gameObject.GetComponent<PlayerModel>().BecomeDead();
        }
    }
}
