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
    private Color myColor;
    
    // Start is called before the first frame update
    void OnEnable()
    {
	    myColor = gameObject.GetComponent<SpriteRenderer>().color;
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        //gameObject.GetComponent<SpriteRenderer>().color.grayscale.Equals(true);
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
        
        if (playerMovement != null)
        {
	        playerMovement.enabled = true;
        }

        gameObject.GetComponent<SpriteRenderer>().color = myColor;
        //gameObject.GetComponent<SpriteRenderer>().color.grayscale.Equals(false);
        gameManager.NewAlive();
    }
}
