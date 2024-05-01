using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
	public StateManager stateManager;
	
    public StateBase taggerState;
    public StateBase runnerState;
    public StateBase deadState;
    
    //public LevelManager levelManager;
    public GameManager gameManager;
    public PlayerMovement playerMovement;

    public TextMeshProUGUI nameTag;
    public SpriteRenderer mySprite;
    
    public List<Color> playerColors;
    private int currentColour;
    
    // Start is called before the first frame update
    void OnEnable()
    {
	    stateManager = GetComponent<StateManager>();
	    gameManager = FindObjectOfType<GameManager>();
	    if (gameManager != null)
	    {
		    gameManager.LevelStartEvent += StartEventGame;
		    gameManager.TaggerWonEvent += TaggerWon;
		    gameManager.RunnerWonEvent += RunnerWon;
	    }
    }
    void OnDisable()
    {
	    if (gameManager != null)
	    {
		    gameManager.LevelStartEvent -= StartEventGame;
		    gameManager.TaggerWonEvent -= TaggerWon;
		    gameManager.RunnerWonEvent -= RunnerWon;
	    }
    }

    private void StartEventGame()
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

    public int SetName(string newName)
    {
	    nameTag.text = newName;
	    return currentColour;
    }

    public void SetColour(int newColour)
    {
	    currentColour = newColour;
	    mySprite.color = playerColors[currentColour];
    }

    public void NextColour()
    {
	    currentColour++;
	    if (currentColour >= playerColors.Count)
	    {
		    currentColour = 0;
	    }

	    mySprite.color = playerColors[currentColour];
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
