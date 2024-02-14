using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OptionsOffState : MonoBehaviour
{
	public GameObject optionsMenu;
	void OnEnable()
	{
		optionsMenu.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
        
	}

	void OnDisable()
	{
		
	}
}
