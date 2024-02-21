using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class DeadState : MonoBehaviour
{
	public GameObject levelManager;
    public MonoBehaviour playerMovement;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (levelManager == null)
        {
            levelManager = GameObject.FindGameObjectWithTag("GameController");
        }
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        levelManager.GetComponent<LevelManager>().NewDead();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        if (levelManager != null)
        {
            levelManager.GetComponent<LevelManager>().NewAlive();
        }
    }
}
