using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TaggerState : StateBase
{
    public GameObject weaponPrefab;
    private GameObject myWeapon;
    
    public override void Enter()
    {
        base.Enter();
        myWeapon = Instantiate(weaponPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public override void Execute()
    {
        base.Execute();
        myWeapon.transform.position = gameObject.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
        Destroy(myWeapon);
    }
}
