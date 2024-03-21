using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput.onActionTriggered += PlayerActionTriggered;
    }

    private void PlayerActionTriggered(InputAction.CallbackContext obj)
    {
        //Debug.Log(obj.action.name);
        if (obj.action.name == "Jump")
        {
            OnJumpPerformed(obj);
        }
        if (obj.action.name == "Horizontal")
        {
            OnHorizontalPerformed(obj);
        }

    }

    private void OnHorizontalPerformed(InputAction.CallbackContext context)
    {
        playerMovement.SetHorizontalInput(context.ReadValue<float>());
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerMovement.OnJumpInput();
        }
        else if (context.canceled)
        {
            playerMovement.OnJumpInputCanceled();
        }
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= PlayerActionTriggered;
    }
}