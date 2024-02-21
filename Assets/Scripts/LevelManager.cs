using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> players;
    private int currentTagger;
    public List<Transform> runnerSpawns;
    public List<Transform> activeSpawns;
    private int currentSpawn;
	public Transform taggerSpawn;
	public int time = 30;
	private int playersDead = 0;
    private bool hasWon = false;
    public MonoBehaviour timerOnState;
	public MonoBehaviour timerOffState;
	
	public event SimpleEvent LevelStart;
	//public event BoolReturnEvent SetTagger;
	public event SimpleEvent TaggerWonEvent;
	public event SimpleEvent RunnerWonEvent;
	
    // Start is called before the first frame update
    void Start()
    {
	    playersDead = 0;
	    LevelStart?.Invoke();
	    
	    //somehow in playerManger.StartGame()
	    players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
	    
	    if(activeSpawns != null)
	    {
		    activeSpawns.Clear();
	    }
	    currentTagger = Random.Range(0,players.Count);
	    //print(players[currentTagger]);
		gameObject.GetComponent<TimerManager>().StartTimer(time);
	    if (players.Count != 0)
	    {
		    //players[currentTagger].transform.position = taggerSpawn.position;
		    players[currentTagger].GetComponent<PlayerManager>().BecomeTagger();
		    for (int i = 0; i < players.Count; i++)
		    {
				SpawnPlayers(i);
			    if (i != currentTagger)
			    {
				    players[i].GetComponent<PlayerManager>().BecomeRunner();
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

    public void SetPlayers(GameObject newPlayer)
    {
	    players.Add(newPlayer);
    }
    private void SpawnPlayers(int currentPlayer)
    {
		currentSpawn = Random.Range(0, players.Count);
		if (activeSpawns == null)
		{
			players[currentPlayer].transform.position = runnerSpawns[currentSpawn].position;
			activeSpawns.Add(runnerSpawns[currentSpawn]);
		}
		else if(!activeSpawns.Contains(runnerSpawns[currentSpawn]))
		{
			players[currentPlayer].transform.position = runnerSpawns[currentSpawn].position;
			activeSpawns.Add(runnerSpawns[currentSpawn]);
		}
		else
		{
			SpawnPlayers(currentPlayer);
		}
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
		    Debug.Log("Tagger Has Won!");
		    hasWon = true;
		    //tagger win event
		    TaggerWonEvent?.Invoke();
	    }
	    if (gameObject.GetComponent<TimerManager>().TimerStatus())
	    {
		    Debug.Log("Runners Has Won!");
		    hasWon = true;
		    //runner win event
		    RunnerWonEvent?.Invoke();
	    }
    }
}
