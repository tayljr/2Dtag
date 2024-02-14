using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public MonoBehaviour menuOnState;
    public MonoBehaviour menuOffState;
    public MonoBehaviour optionsOnState;
    public MonoBehaviour optionsOffState;
    private bool menuOn = false;
    private bool optionsOn = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuToggle();
        }
    }

    public void MenuToggle()
    {
            if (!menuOn)
            {
                GetComponent<StateManager>().ChangeState(menuOnState);
                menuOn = true;
            } 
            else if (menuOn)
            {
                GetComponent<StateManager>().ChangeState(optionsOffState);
                GetComponent<StateManager>().ChangeState(menuOffState);
                menuOn = false;
            }
    }

    public void OptionsToggle()
    {
        if (!optionsOn)
        {
            GetComponent<StateManager>().ChangeState(menuOffState);
            GetComponent<StateManager>().ChangeState(optionsOnState);
            optionsOn = true;
        }
        else if (optionsOn)
        {
            GetComponent<StateManager>().ChangeState(menuOnState);
            GetComponent<StateManager>().ChangeState(optionsOffState);
            optionsOn = false;
        }
    }
}

