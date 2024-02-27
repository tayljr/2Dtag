using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinUI : MonoBehaviour
{
	public MonoBehaviour taggerWonState;
	public MonoBehaviour runnerWinState;
	public GameManager gameManager;
	public StateManager stateManager;
	
    // Start is called before the first frame update
    void OnEnable()
    {
	    gameManager.TaggerWonEvent += TaggerWon;
	    gameManager.RunnerWonEvent += RunnerWon;
    }

    private void OnDisable()
    {
	    gameManager.TaggerWonEvent -= TaggerWon;
    }

    private void TaggerWon()
    {
	    stateManager.ChangeState(taggerWonState);
    }
    private void RunnerWon()
    {
	    stateManager.ChangeState(runnerWinState);
    }
}
