using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftObject : MonoBehaviour, IActionable
{
	private SpriteRenderer sprite;

	private BoxCollider2D collision;

	private bool isActive = true;
	
    // Start is called before the first frame update
    void Start()
    {
	    sprite = GetComponent<SpriteRenderer>();
	    collision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
	    Shift();
    }

    public void Deactivate()
    {
	    Shift();
    }

    private void Shift()
    {
	    isActive = !isActive;
	    collision.enabled = isActive;
	    if (isActive)
	    {
		    Color tmp = sprite.color;
		    tmp.a = 0.75f;
		    sprite.color = tmp;
	    }
	    else
	    {
		    Color tmp = sprite.color;
		    tmp.a = 0.05f;
		    sprite.color = tmp;
	    }
    }
}
