using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpingPower = 18f;
    private bool isMoving = false;
    private bool isRight = true;
    private float direction = 0;
    
    public bool isAlive = true;
    
    public Rigidbody2D rb;
    private MainControlls mainControls;

    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    private void Awake()
    {
        mainControls = new MainControlls();
        mainControls.Enable();
    }

    private void OnEnable()
    {
        mainControls.Main.Horizontal.performed += HorizontalOnPerfirmed;
        mainControls.Main.Horizontal.canceled += HorizontalOnPerfirmed;
        mainControls.Main.Jump.performed += VerticalOnPerfirmed;
        mainControls.Main.Jump.canceled += VerticalOnPerfirmed;
    }
    
    private void OnDisable()
    {
        mainControls.Main.Horizontal.performed -= HorizontalOnPerfirmed;
        mainControls.Main.Horizontal.canceled -= HorizontalOnPerfirmed;
        mainControls.Main.Jump.performed -= VerticalOnPerfirmed;
        mainControls.Main.Jump.canceled -= VerticalOnPerfirmed;
    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }
    private void HorizontalOnPerfirmed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<float>();
    }
    
    private void VerticalOnPerfirmed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            if(IsGrounded()){
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
        }else if (!obj.performed)
        {
            if(rb.velocity.y > 0f){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f);
            }
        }
        
    }

    private bool IsGrounded()
    {
        return rb.velocity.y < 0.001f && rb.velocity.y > -0.001f;
    }
}
