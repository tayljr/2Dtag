using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TaggerWonState : MonoBehaviour
{
	public GameObject taggerWinMenu;
	void OnEnable()
	{
		taggerWinMenu.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
	    
	}

	void OnDisable()
	{
	    
	}
}
