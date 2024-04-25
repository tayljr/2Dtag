using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestYes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(bool on)
    {
	    if(on)
	    {
		    Debug.Log("yes");
	    }
	    else
	    {
		    Debug.Log("no");
	    }
    }

    public void Deactivate()
    {
	    Debug.Log("no");
    }
}
