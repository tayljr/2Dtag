using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Keyboard;

public class PlayerSpawnManager : MonoBehaviour
{
    public PlayerInputManager playerInputManager;
    private List<GameObject> activePlayers;
    public GameObject playerPrefab;
    
    private MainControlls mainControls;
    private void Awake()
    {
        //mainControls = new MainControlls();
        //mainControls.Enable();

        //PlayerInput player1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
        //var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard2");
    }
    private void OnEnable()
    {
        //mainControls.Main.Jump.performed += NewPlayer;
        //playerInputManager.onPlayerJoined += PlayerJoined;
    }

    private void PlayerJoined(PlayerInput obj)
    {
        //obj.SwitchCurrentControlScheme("Keyboard1");
    }

    private void OnDisable()
    {
        //mainControls.Main.Jump.performed -= NewPlayer;
        //playerInputManager.onPlayerJoined += PlayerJoined;

    }
    
    private void NewPlayer(InputAction.CallbackContext obj)
    {
       //Debug.Log(obj);
    }
// need to detect what button was used to spawn the player
}
