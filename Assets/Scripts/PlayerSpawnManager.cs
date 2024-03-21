using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Keyboard;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab
    public Transform spawnPoint; // The point where players will spawn
    private int currentPlayerIndex; // Track the current player index
    
    private PlayerInputManager playerInputManager;
    private void Awake()
    {
    }
    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += NewPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= NewPlayer;

    }
    
    private void NewPlayer(PlayerInput playerInput)
    {
    }
}
