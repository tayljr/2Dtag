using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using InputDevice = UnityEngine.XR.InputDevice;
using Random = UnityEngine.Random;

public class PlayerSpawnManager : FunctionManager
{
    public List<Transform> playerSpawns; // The points where players will spawn
    public List<Transform> activeSpawns;
    private int currentSpawn;
    private int currentPlayerIndex;
    private int currentKeyboardIndex;
    public List<PlayerInput> players;
    public List<GameObject> playersGO;
    public PlayerInputManager playerInputManager;
    public GameObject nameInput;
    public GameObject startButton;
    public TMP_InputField userInput;
    public GameObject playerListPrefab;
    public Transform playerList;
    private GameObject currentPlayerList;
    private int readyPlayers;
    private List<string> playerNames = new List<string>();
    public List<Color> playerColors;
    private List<string> playerControllerSchemes = new List<string>();
    private List<string> keyboardControllerSchemes = new List<string>();
    private bool inLobby;
    //private KeyboardSplitter keyboardSplitter;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        currentPlayerIndex = 0;
        currentKeyboardIndex = 0;
        nameInput.SetActive(false);
        startButton.SetActive(false);
        playerInputManager.onPlayerJoined += NewPlayer;
        SceneManager.sceneLoaded += NewScene;
        SceneManager.sceneUnloaded += OldScene;
    }


    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= NewPlayer;
        SceneManager.sceneLoaded -= NewScene;
        SceneManager.sceneUnloaded -= OldScene;
        //currentPlayerList.GetComponentInChildren<Toggle>().onValueChanged.RemoveAllListeners();
    }

    private void Start()
    {
    }

    private void OldScene(Scene scene)
    {
        if (!scene.Equals(SceneManager.GetSceneByName("Lobby")) &&
            !scene.Equals(SceneManager.GetSceneByName("MainMenu")))
        {
            Destroy(gameObject);
        }
    }

    //most likely won't spawn players at set locations due to not being able to get playerSpawns locations
    //done i think
    private void NewScene(Scene scene, LoadSceneMode mode)
    {
        //when a level loads
        playerInputManager.joiningEnabled.Equals(players.Count == 0);
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        //keyboardSplitter = FindObjectOfType<KeyboardSplitter>();
        if (!scene.Equals(SceneManager.GetSceneByName("Lobby")) && !scene.Equals(SceneManager.GetSceneByName("MainMenu")))
        {
            inLobby = false;
            playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
            playerInputManager.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
            playerInputManager.onPlayerJoined += NewPlayer;
            PlayerSpawner[] findSpawns = FindObjectsOfType<PlayerSpawner>();
            for (int i = 0; i < findSpawns.Length; i++)
            {
                playerSpawns.Add(findSpawns[i].gameObject.transform);
            }

            activeSpawns.Clear();
            currentKeyboardIndex = 0;
            for (int i = 0; i < players.Count; i++)
            {
                currentPlayerIndex = i;
                SpawnPlayers(i);
            }

            FindObjectOfType<GameManager>().PlayersLoaded();
        }
        else if (scene.Equals(SceneManager.GetSceneByName("Lobby")))
        {
            inLobby = true;
            playerNames.Clear();
            playerSpawns.Clear();
            activeSpawns.Clear();
            currentSpawn = 0;
            currentPlayerIndex = 0;
            currentKeyboardIndex = 0;
            players.Clear();
            playersGO.Clear();
            readyPlayers = 0;
            playerControllerSchemes.Clear();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SpawnPlayers(int currentPlayer)
    {
        currentSpawn = Random.Range(0, players.Count);
        if (activeSpawns.Count == 0)
        {
            playerInputManager.JoinPlayer(currentPlayer, -1, playerControllerSchemes[currentPlayer]);
            if (playerControllerSchemes[currentPlayer] == "Keyboard")
            {
                //keyboardSplitter.players[currentKeyboardIndex].name = keyboardControllerSchemes[currentKeyboardIndex];
                currentKeyboardIndex++;
            }
            //needs to instatiate new playes and give them the correct names and controll scheemes
            //Done i think
            activeSpawns.Add(playerSpawns[currentSpawn]);
        }
        else if (!activeSpawns.Contains(playerSpawns[currentSpawn]))
        {
            playerInputManager.JoinPlayer(currentPlayer, -1, playerControllerSchemes[currentPlayer]);
            activeSpawns.Add(playerSpawns[currentSpawn]);
        }
        else
        {
            SpawnPlayers(currentPlayer);
        }
    }

    private void NewPlayer(PlayerInput playerInput)
    {
        if (inLobby)
        {
            playerInputManager.DisableJoining();
            userInput.text = "";
            nameInput.SetActive(true);
            //Debug.Log(playerInput);
            players.Add(playerInput);
            playerControllerSchemes.Add((playerInput.currentControlScheme));
            if (playerInput.currentControlScheme == "Keyboard")
            {
                //keyboardControllerSchemes.Add(keyboardSplitter.players[currentKeyboardIndex].name);
                Debug.Log(playerInput.devices);
                currentKeyboardIndex++;
            }
            players[currentPlayerIndex].GetComponent<PlayerModel>().SetColour(playerColors[currentPlayerIndex]);
            foreach (PlayerInput player in players)
            {
                player.SwitchCurrentActionMap("Menu");
            }
            CheckReady();
        }
        else
        {
            playersGO.Add(playerInput.gameObject);
            playersGO[currentPlayerIndex].transform.position = playerSpawns[currentSpawn].position;
            playersGO[currentPlayerIndex].GetComponent<PlayerModel>().SetColour(playerColors[currentPlayerIndex]);
            playersGO[currentPlayerIndex].GetComponent<PlayerModel>().SetName(playerNames[currentPlayerIndex]);
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
        if (ready)
        {
            readyPlayers++;
        }
        else
        {
            readyPlayers--;
        }
        CheckReady();
    }

    private void CheckReady()
    {
        //min two player
        //TODO maybe min player variable
        if (readyPlayers == players.Count && players.Count >= 2)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
        //Debug.Log(readyPlayers + " : " + players.Count);
    }
}