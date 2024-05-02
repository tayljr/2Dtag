using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour, IActionable
{
	private SpriteRenderer sprite;

	private BoxCollider2D collision;

	[SerializeField] private bool isActive = true;
	
	//private bool isActive;
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

    public void Deactivate()
    {
	    
    }
}
