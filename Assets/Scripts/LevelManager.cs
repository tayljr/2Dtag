using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> players;
    private int currentTagger;
    public List<Transform> runnerSpawns;
	public Transform taggerSpawn;
	public int time = 30;
	private int playersDead = 0;
    private bool hasWon = false;
    public MonoBehaviour timerOnState;
	public MonoBehaviour timerOffState;
	public MonoBehaviour TaggerWonState;
	public MonoBehaviour TaggerLoseState;
    // Start is called before the first frame update
    void Start()
    {
	    playersDead = 0;
	    players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
	    currentTagger = Random.Range(0,players.Count);
	    //print(players[currentTagger]);
	    if (players.Count != 0)
	    {
		    gameObject.GetComponent<TimerManager>().StartTimer(time);
		    players[currentTagger].transform.position = taggerSpawn.position;
		    players[currentTagger].GetComponent<PlayerManager>().BecomeTagger();
		    for (int i = 0; i < players.Count; i++)
		    {
			    if (i != currentTagger)
			    {
				    players[i].transform.position = runnerSpawns[i].position;
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
		    GetComponent<StateManager>().ChangeState(TaggerWonState);
	    }
	    if (gameObject.GetComponent<TimerManager>().TimerStatus())
	    {
		    Debug.Log("Runners Has Won!");
		    hasWon = true;
		    GetComponent<StateManager>().ChangeState(TaggerLoseState);
	    }
    }
}
