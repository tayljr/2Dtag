using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestYes : MonoBehaviour, IActionable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
	    gameObject.SetActive(true);
    }

    public void Deactivate()
    {
	    gameObject.SetActive(false);
    }
}
