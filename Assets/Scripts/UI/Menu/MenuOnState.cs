using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuOnState : MonoBehaviour
{
	public GameObject pauseMenu;
    void OnEnable()
    {
	    pauseMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
	    
    }

    void OnDisable()
    {
	    
    }
}
