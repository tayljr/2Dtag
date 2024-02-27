using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class DeadState : MonoBehaviour
{
	public GameManager gameManager;
    public MonoBehaviour playerMovement;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        gameManager.NewDead();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.NewAlive();
        }
    }
}
