using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunnerState : MonoBehaviour
{
	public Collider2D weapon;
    
    // Start is called before the first frame update
    void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == weapon && this.enabled)
        {
            gameObject.GetComponent<PlayerModel>().BecomeDead();
        }
    }
}
