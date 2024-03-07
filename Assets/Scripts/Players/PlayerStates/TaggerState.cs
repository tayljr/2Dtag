using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TaggerState : MonoBehaviour
{
    public GameObject weaponPrefab;
    private GameObject myWeapon;
    
    // Start is called before the first frame update
    void OnEnable()
    {
	    myWeapon = Instantiate(weaponPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        myWeapon.transform.position = gameObject.transform.position;
    }

    private void OnDisable()
    {
	    Destroy(myWeapon);
    }
}
