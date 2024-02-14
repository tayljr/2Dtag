using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class optionsOnState : MonoBehaviour
{
	public GameObject optionsMenu;
    void OnEnable()
    {
	    optionsMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
	    
    }

    void OnDisable()
    {
	    
    }
}
