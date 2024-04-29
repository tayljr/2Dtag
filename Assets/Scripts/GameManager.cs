using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : FunctionManager
{
    public List<GameObject> players;
    private int currentTagger;
	public int time = 30;
	private int playersDead = 0;
    private bool hasWon = false;
    public TimerManager timerManger;
    public MonoBehaviour timerOnState;
	public MonoBehaviour timerOffState;

	private PlayerInput playerInput;
	
	public event SimpleEvent LevelStartEvent;
	//public event BoolReturnEvent SetTaggerEvent;
	public event SimpleEvent TaggerWonEvent;
	public event SimpleEvent RunnerWonEvent;
	public event SimpleEvent PauseGameEvent;
	public event SimpleEvent PlayGameEvent;

	private void Awake()
	{
		
	}

	// Start is called before the first frame update
    public void PlayersLoaded()
    {
	    playersDead = 0;
	    LevelStartEvent?.Invoke();
	    
	    currentTagger = Random.Range(0,players.Count);
	    //print(players[currentTagger]);
	    timerManger.StartTimer(time);
	    if (players.Count != 0)
	    {
		    //players[currentTagger].transform.position = taggerSpawn.position;
		    players[currentTagger].GetComponent<PlayerModel>().BecomeTagger();
		    for (int i = 0; i < players.Count; i++)
		    {
				//SpawnPlayers(i);
			    if (i != currentTagger)
			    {
				    players[i].GetComponent<PlayerModel>().BecomeRunner();
			    }
		    }
	    }
    }


    // Update is called once per frame
    void Update()
    {
	    if(!hasWon)
	    {
		    WinCheck();
	    }
    }
    
    public void GamePause()
    {
	    PauseGameEvent?.Invoke();
    }
    
    public void GamePlay()
    {
	    PlayGameEvent?.Invoke();
    }


    public void SetPlayers(GameObject newPlayer)
    {
	    players.Add(newPlayer);
    }
    
    public void NewDead()
    {
	    playersDead++;
    }

    public void NewAlive()
    {
	    playersDead--;
    }
    
    private void WinCheck()
    {
	    if (playersDead == players.Count - 1)
	    {
		    //Debug.Log("Tagger Has Won!");
		    hasWon = true;
		    timerManger.StopTimer();
		    //tagger win event
		    TaggerWonEvent?.Invoke();
	    }
	    if (timerManger.TimerStatus())
	    {
		    //Debug.Log("Runners Has Won!");
		    hasWon = true;
		    timerManger.StopTimer();
		    //runner win event
		    RunnerWonEvent?.Invoke();
	    }
    }
}