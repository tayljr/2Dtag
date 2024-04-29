using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameManager gameManager;
    public StateManager stateManager;
    public StateBase menuOnState;
    public StateBase menuOffState;
    public StateBase optionsOnState;
    public StateBase optionsOffState;
    private bool menuOn = false;
    private bool optionsOn = false;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        stateManager = GetComponent<StateManager>();
    }

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
                stateManager.ChangeState(menuOnState);
                gameManager?.GamePause();
                menuOn = true;
            } 
            else if (menuOn)
            {
                stateManager.ChangeState(optionsOffState);
                stateManager.ChangeState(menuOffState);
                gameManager?.GamePlay();
                menuOn = false;
            }
    }

    public void OptionsToggle()
    {
        if (!optionsOn)
        {
            stateManager.ChangeState(menuOffState);
            stateManager.ChangeState(optionsOnState);
            optionsOn = true;
        }
        else if (optionsOn)
        {
            stateManager.ChangeState(menuOnState);
            stateManager.ChangeState(optionsOffState);
            optionsOn = false;
        }
    }
}

