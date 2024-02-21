using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinUI : MonoBehaviour
{
	public MonoBehaviour taggerWonState;
	[FormerlySerializedAs("taggerLoseState")]
	public MonoBehaviour runnerWinState;
	
	public LevelManager levelManager;
	
    // Start is called before the first frame update
    void OnEnable()
    {
	    levelManager.TaggerWonEvent += TaggerWon;
	    levelManager.RunnerWonEvent += RunnerWon;
    }

    private void OnDisable()
    {
	    levelManager.TaggerWonEvent -= TaggerWon;
    }

    private void TaggerWon()
    {
	    GetComponent<StateManager>().ChangeState(taggerWonState);
    }
    private void RunnerWon()
    {
	    GetComponent<StateManager>().ChangeState(runnerWinState);
    }
}
