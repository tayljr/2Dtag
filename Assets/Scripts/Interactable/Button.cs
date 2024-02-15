using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Button : SerializedMonoBehaviour
{
	public List<IActionable> actionables;
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
		    foreach (var actionable in actionables)
		    {
			    actionable.Activate();
		    }
		    isActive = true;
	    } 
	    else if (weight <= deactivateWeight && isActive)
	    {
		    foreach (var actionable in actionables)
		    {
			    actionable.Deactivate();
		    }
		    isActive = false;
	    }
    }
}
