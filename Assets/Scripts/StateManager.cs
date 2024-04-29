using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
	//make base state class
    public StateBase startingState;
    public StateBase currentState;
    public List<StateBase> states;
 
    // Set a default state
    private void OnEnable()
    {
        if (startingState != null)
        {
            ChangeState(startingState);
        }
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    // This works for ANY STATE
    public void ChangeState(StateBase newState)
    {
        // Check if the state is the same and DON'T swap
        if (newState == currentState)
        {
            return;
        }

        // At first 'currentstate' will ALWAYS be null
        if (currentState != null)
        {
            currentState.Exit();
        }

        newState.Enter();

        // New state swap over to incoming state
        currentState = newState;
    }
}
