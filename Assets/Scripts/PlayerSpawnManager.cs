using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using InputDevice = UnityEngine.XR.InputDevice;
using Random = UnityEngine.Random;
using Toggle = UnityEngine.UI.Toggle;

public class PlayerSpawnManager : FunctionManager
{
    public List<Transform> playerSpawns; // The points where players will spawn
    public List<Transform> activeSpawns;
    private int currentSpawn;

    private int currentPlayerIndex;

    // private int currentKeyboardIndex;
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

    private List<int> playerColours = new List<int>();

    // public List<Color> playerColors;
    private List<string> playerControllerSchemes = new List<string>();

    // private List<string> playerDevices = new List<string>();
    private bool inLobby;
    // private PlayerInput currentPlayerInput;
    // private KeyboardSplitter keyboardSplitter;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (playerInputManager == null)
        {
            playerInputManager = FindObjectOfType<PlayerInputManager>();
        }

        currentPlayerIndex = 0;
        // currentKeyboardIndex = 0;
        nameInput.SetActive(false);
        startButton.SetActive(false);
        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined += NewPlayer;
        }
        SceneManager.sceneLoaded += NewScene;
        SceneManager.sceneUnloaded += OldScene;
    }


    private void OnDisable()
    {
        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined -= NewPlayer;
        }
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
            playerInputManager.onPlayerJoined -= NewPlayer;
            SceneManager.sceneLoaded -= NewScene;
            SceneManager.sceneUnloaded -= OldScene;
            playerNames.Clear();
            playerSpawns.Clear();
            activeSpawns.Clear();
            currentSpawn = 0;
            currentPlayerIndex = 0;
            // currentKeyboardIndex = 0;
            players.Clear();
            playersGO.Clear();
            readyPlayers = 0;
            playerControllerSchemes.Clear();
            Destroy(gameObject);
        }
    }

    //most likely won't spawn players at set locations due to not being able to get playerSpawns locations
    //done i think
    private void NewScene(Scene scene, LoadSceneMode mode)
    {
        //when a level loads
        if (playerInputManager == null)
        {
            playerInputManager = FindObjectOfType<PlayerInputManager>();
        }

        if (playerInputManager != null)
        {
            playerInputManager.joiningEnabled.Equals(players.Count == 0);
        }

        // keyboardSplitter = FindObjectOfType<KeyboardSplitter>();
        if (!scene.Equals(SceneManager.GetSceneByName("Lobby")) &&
            !scene.Equals(SceneManager.GetSceneByName("MainMenu")))
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
            // currentKeyboardIndex = 0;
            for (int i = 0; i < players.Count; i++)
            {
                currentPlayerIndex = i;
                SpawnPlayers(i);
            }

            FindObjectOfType<GameManager>().PlayersLoaded();
        }
        else if (scene.Equals(SceneManager.GetSceneByName("Lobby")))
        {
            if (playerInputManager == null)
            {
                playerInputManager = GetComponent<PlayerInputManager>();
            }

            inLobby = true;
            playerNames.Clear();
            playerSpawns.Clear();
            activeSpawns.Clear();
            currentSpawn = 0;
            currentPlayerIndex = 0;
            // currentKeyboardIndex = 0;
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
            playerInputManager.JoinPlayer(currentPlayer, -1, playerControllerSchemes[currentPlayerIndex]);
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
            userInput.text = "Player " + (currentPlayerIndex + 1);
            nameInput.SetActive(true);
            nameInput.GetComponentInChildren<TMP_InputField>().ActivateInputField();
            //Debug.Log(playerInput);
            players.Add(playerInput);
            playerControllerSchemes.Add((playerInput.currentControlScheme));
            // currentPlayerInput = playerInput;
            // if (playerInput.currentControlScheme == "Keyboard")
            // {
            //     string device = playerInput.devices[0].ToString();
            //     string keyboard = "Keyboard:/";
            //     device = device.Replace(keyboard, "");
            //     playerDevices.Add(device);
            //     Debug.Log(playerInput.devices[0]);
            //     currentKeyboardIndex++;
            // }
            players[currentPlayerIndex].GetComponent<PlayerModel>().SetColour(currentPlayerIndex);
            foreach (PlayerInput player in players)
            {
                player.SwitchCurrentActionMap("Menu");
            }

            CheckReady();
        }
        else
        {
            playersGO.Add(playerInput.gameObject);
            // if (playerControllerSchemes[currentPlayerIndex] == "Keyboard")
            // {
            //     keyboardSplitter.players[currentKeyboardIndex].routes[0].remapped = 
            //     currentKeyboardIndex++;
            // }
            playersGO[currentPlayerIndex].transform.position = playerSpawns[currentSpawn].position;
            playersGO[currentPlayerIndex].GetComponent<PlayerModel>().SetColour(playerColours[currentPlayerIndex]);
            playersGO[currentPlayerIndex].GetComponent<PlayerModel>().SetName(playerNames[currentPlayerIndex]);
            //playersGO[currentPlayerIndex].GetComponent<PlayerInput>().SwitchCurrentControlScheme(playerControllerSchemes[currentPlayerIndex], playerDevices[currentPlayerIndex]);
        }
    }

    public void ConfirmName()
    {
        playerInputManager.EnableJoining();
        nameInput.SetActive(false);
        //Debug.Log(userInput.text);

        //set name returns that players current colour int
        playerColours.Add(players[currentPlayerIndex].GetComponent<PlayerModel>().SetName(userInput.text));
        currentPlayerList = Instantiate(playerListPrefab, playerList);
        currentPlayerList.GetComponentInChildren<TextMeshProUGUI>().text = userInput.text;
        playerNames.Add(userInput.text);
        //playerDevices.Add(currentPlayerInput.devices[0]);
        foreach (PlayerInput player in players)
        {
            player.SwitchCurrentActionMap("Main");
        }

        currentPlayerList.GetComponentInChildren<Toggle>().onValueChanged.AddListener(NewReady);
        currentPlayerIndex++;
    }

    private void NewReady(bool ready)
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

    public void NextColour()
    {
        players[currentPlayerIndex].GetComponent<PlayerModel>().NextColour();
    }
}