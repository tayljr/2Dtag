using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform spawnPoint; // The point where players will spawn
    private int currentPlayerIndex;
    public List<PlayerInput> players;
    public PlayerInputManager playerInputManager;
    public GameObject nameInput;
    public TMP_InputField userInput;
    public GameObject playerListPrefab;
    public Transform playerList;
    private GameObject currentPlayerList;
   
    private void OnEnable()
    {
        currentPlayerIndex = 0;
        nameInput.SetActive(false);
        playerInputManager.onPlayerJoined += NewPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= NewPlayer;
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
        foreach (PlayerInput player in players)
        {
            player.SwitchCurrentActionMap("Main");
        }

        currentPlayerIndex++;
    }
}
