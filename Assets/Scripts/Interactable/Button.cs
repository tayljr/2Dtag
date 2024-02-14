using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class Button : SerializedMonoBehaviour
{
	public IActionable actionable;
	public int activateWeight;
	public int deactivateWeight;
	private int weight;
	private bool isActive;
    public void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.gameObject.CompareTag("Player"))
	    {
		    weight++;
		    CheckWeight();
	    }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
	    if (other.gameObject.CompareTag("Player"))
	    {
		    weight--;
		    CheckWeight();

	    }
    }

    private void CheckWeight()
    {
	    if (weight >= activateWeight && !isActive)
	    {
		    actionable.Activate();
		    isActive = true;
	    } else if (weight <= deactivateWeight && isActive)
	    {
		    actionable.Deactivate();
		    isActive = false;
	    }
    }
}
