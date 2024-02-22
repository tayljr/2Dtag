using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RunnerWinState : MonoBehaviour
{
	public GameObject runnerWinMenu;
	void OnEnable()
	{
		runnerWinMenu.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
	    
	}

	void OnDisable()
	{
	    
	}
}
