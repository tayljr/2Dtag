using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TimerOnState : MonoBehaviour
{
	public GameObject timer;
	void OnEnable()
	{
		timer.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
	    
	}

	void OnDisable()
	{
		if(timer)
		{
			GetComponent<TimerManager>().StopTimer();
			timer.SetActive(false);
		}
	}
}
