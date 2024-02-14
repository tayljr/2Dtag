using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TaggerState : MonoBehaviour
{
    public GameObject weapon;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        if(weapon == null){
            weapon = GameObject.FindWithTag("TagThing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        weapon.transform.position = gameObject.transform.position;
    }

    private void OnDisable()
    {
	    weapon = null;
    }
}
