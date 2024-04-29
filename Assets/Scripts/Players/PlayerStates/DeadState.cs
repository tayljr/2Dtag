using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class DeadState : StateBase
{
	public GameManager gameManager;
    public MonoBehaviour playerMovement;
    public SpriteRenderer mySprite;
    private Color myColor;
    
    public override void Enter()
    {
        base.Enter();
        gameManager = FindObjectOfType<GameManager>();
	    myColor = mySprite.color;
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        mySprite.color = Color.gray;
        if (gameManager != null)
        {
            gameManager.NewDead();
        }
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
        if (gameManager != null)
        {
            gameManager.NewAlive();
        }
        
        if (playerMovement != null)
        {
	        playerMovement.enabled = true;
        }

        mySprite.color = myColor;
    }
}
