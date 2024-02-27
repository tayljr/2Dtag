using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public MonoBehaviour taggerState;
    public MonoBehaviour runnerState;
    public MonoBehaviour deadState;
    
    //public LevelManager levelManager;
    public GameManager gameManager;
    public PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void OnEnable()
    {
	    gameManager.LevelStart += StartGame;
	    gameManager.TaggerWonEvent += TaggerWon;
	    gameManager.RunnerWonEvent += RunnerWon;
    }
    void OnDisable()
    {
	    gameManager.LevelStart -= StartGame;
	    gameManager.TaggerWonEvent -= TaggerWon;
	    gameManager.RunnerWonEvent -= RunnerWon;
    }

    private void StartGame()
    {
	    gameManager.SetPlayers(gameObject);
    }

    private void TaggerWon()
    {
	    playerMovement.enabled = false;
    }private void RunnerWon()
    {
	    playerMovement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BecomeTagger()
    {
        gameObject.GetComponent<StateManager>().ChangeState(taggerState);
    }
    
    public void BecomeRunner()
    {
        gameObject.GetComponent<StateManager>().ChangeState(runnerState);
    }
    
    public void BecomeDead()
    {
        gameObject.GetComponent<StateManager>().ChangeState(deadState);
    }
}
