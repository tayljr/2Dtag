using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpingPower = 18f;
    private float direction = 0;
    public float groundHeight = 0.2f;
    public GameObject groundCheck;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private float slopeAngle;

    
    //https://www.youtube.com/watch?v=DrFk5Q_IwG0
    public GameObject stepRay;
    public float stepHeight = 0.5f;
    public float stepPower = 1.5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        
        slopeAngle = GetSlopeAngle();
        //Debug.Log(slopeAngle);
        
        //if not on a slope
        if (slopeAngle <= 0)
        {
            StepClimb();
        }
    }
    
    public void SetHorizontalInput(float input)
    {
        direction = input;
        if (direction < 0)
        {
            spriteRenderer.flipY = true;
        } else if (direction > 0)
        {
            spriteRenderer.flipY = false;
        }
    }

    public void OnJumpInput()
    {
        // if is grounded 
        if (slopeAngle != -1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    public void OnJumpInputCanceled()
    {
        if (rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f);
        }
    }
    
    
    //is grounded plus slope angle returns a -1 if the player is not grounded
    private float GetSlopeAngle()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(groundCheck.transform.position, new Vector2(1.15f, 0.1f), 0f, Vector2.down, groundHeight);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(groundCheck.transform.position + new Vector3(1.15f / 2, 0), Vector2.down * (0.1f + groundHeight), rayColor);
        Debug.DrawRay(groundCheck.transform.position - new Vector3(1.15f / 2, 0), Vector2.down * (0.1f + groundHeight), rayColor);
        Debug.DrawRay(groundCheck.transform.position - new Vector3(1.15f / 2, 0.1f + groundHeight), Vector2.right * (1.15f), rayColor);
        if (raycastHit.collider != null)
        {
            return Vector2.Angle(raycastHit.normal, Vector2.up);
        }
        return -1;
    }

    private void StepClimb()
    {
        RaycastHit2D hitLower = Physics2D.Raycast(new Vector2(stepRay.transform.position.x, stepRay.transform.position.y), Vector2.right * direction, 1f);
        Color lowerColour;
        if (hitLower.collider != null)
        {
            float stepTop = hitLower.collider.bounds.max.y;
            lowerColour = Color.blue;
            float heightDifference = stepTop - stepRay.transform.position.y;
            if (heightDifference <= stepHeight)
            {
                lowerColour = Color.green;
                
                //velocity required to reach a certain height (square root of 2 x gravity x the height) 
                float requiredVelocity = Mathf.Sqrt(2 * rb.gravityScale * heightDifference);
                rb.velocity = new Vector3(rb.velocity.x, (rb.velocity.y + requiredVelocity) * stepPower);
            }
        }
        else
        {
            lowerColour = Color.red;
        }
        Debug.DrawRay(new Vector2(stepRay.transform.position.x, stepRay.transform.position.y), Vector2.right * (direction * 1f), lowerColour);
    }
}
