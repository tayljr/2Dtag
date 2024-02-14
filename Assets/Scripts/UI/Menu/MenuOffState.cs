using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuOffState : MonoBehaviour
{
	public GameObject pauseMenu;
	void OnEnable()
	{
		pauseMenu.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
        
	}

	void OnDisable()
	{

	}
}
