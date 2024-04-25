using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerSpawnManager : FunctionManager
{
    public List<Transform> playerSpawns;// The points where players will spawn
    public List<Transform> activeSpawns; 
    private int currentSpawn;
    private int currentPlayerIndex;
    public List<PlayerInput> players;
    public PlayerInputManager playerInputManager;
    public GameObject nameInput;
    public GameObject startButton;
    public TMP_InputField userInput;
    public GameObject playerListPrefab;
    public Transform playerList;
    private GameObject currentPlayerList;
    private int readyPlayers;
    private List<string> playerNames;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        currentPlayerIndex = 0;
        nameInput.SetActive(false);
        startButton.SetActive(false);
        playerInputManager.onPlayerJoined += NewPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= NewPlayer;
        currentPlayerList.GetComponentInChildren<Toggle>().onValueChanged.RemoveAllListeners();
    }

    private void Start()
    {
        //when a level loads
        if (players.Count != 0)
        {
            playerInputManager.joiningEnabled.Equals(players.Count != 0);
            for (int i = 0; i < players.Count; i++)
            {
                SpawnPlayers(i);
            }
        }
        else
        {
            playerInputManager.joiningEnabled.Equals(true);
        }
    }

    
    //most likely won't spawn players at set locations due to not being able to get playerSpawns locations
    private void SpawnPlayers(int currentPlayer)
    {
        currentSpawn = Random.Range(0, players.Count);
        if (activeSpawns == null)
        {
            //needs to instatiate new playes and give them the correct names and controll scheemes
            players[currentPlayer].transform.position = playerSpawns[currentSpawn].position;
            activeSpawns.Add(playerSpawns[currentSpawn]);
        }
        else if(!activeSpawns.Contains(playerSpawns[currentSpawn]))
        {
            players[currentPlayer].transform.position = playerSpawns[currentSpawn].position;
            activeSpawns.Add(playerSpawns[currentSpawn]);
        }
        else
        {
            SpawnPlayers(currentPlayer);
        }
    }
    
    private void NewPlayer(PlayerInput playerInput)
    {
        playerInputManager.DisableJoining();
        userInput.text = "";
        nameInput.SetActive(true);
        //Debug.Log(playerInput);
        players.Add(playerInput);
        foreach (PlayerInput player in players)
        {
            player.SwitchCurrentActionMap("Menu");
        }
    }

    public void ConfirmName()
    {
        playerInputManager.EnableJoining();
        nameInput.SetActive(false);
        //Debug.Log(userInput.text);
        players[currentPlayerIndex].GetComponent<PlayerModel>().SetName(userInput.text);
        currentPlayerList = Instantiate(playerListPrefab, playerList);
        currentPlayerList.GetComponentInChildren<TextMeshProUGUI>().text = userInput.text;
        playerNames.Add(userInput.text);
        foreach (PlayerInput player in players)
        {
            player.SwitchCurrentActionMap("Main");
        }
        currentPlayerList.GetComponentInChildren<Toggle>().onValueChanged.AddListener(newReady);
        currentPlayerIndex++;
    }

    private void newReady(bool ready)
    {
        if(ready)
        {
            readyPlayers++;
        }
        else
        {
            readyPlayers--;
        }

        if (readyPlayers == players.Count)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
        Debug.Log(readyPlayers + " : " + players.Count);
    }
}
