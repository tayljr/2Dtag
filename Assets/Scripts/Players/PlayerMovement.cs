using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpingPower = 18f;
    private bool isMoving = false;
    private bool isRight = true;
    
    public bool isAlive = true;
    /*public bool tagger = false;
    public GameObject tagThing;*/
    //change these to different states
    
    public Rigidbody2D rb;

    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(rightKey)){
            RightPressed();
        }
        if(Input.GetKey(leftKey)){
            LeftPressed();
        }
        
        if(Input.GetKeyUp(rightKey)){
            RightReleased();
        }
        if(Input.GetKeyUp(leftKey)){
            LeftReleased();
        }

        if(Input.GetKeyDown(upKey)){
            UpPressed();
        }
        if(Input.GetKeyUp(upKey)){
            UpReleased();
        }

        if(Input.GetKeyDown(downKey)){
            DownPressed();
        }
        if(Input.GetKeyUp(downKey)){
            DownReleased();
        }
    }

    private bool IsGrounded()
    {
        return rb.velocity.y < 0.001f && rb.velocity.y > -0.001f;
    }

    private void RightPressed()
    {
        if(rb.velocity.x >= -speed * 0.2f){
            rb.velocity = new Vector2(speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
            isMoving = true;
            isRight = true;
        }
    }

    private void RightReleased()
    {
        if(isMoving && isRight){
            rb.velocity = new Vector2(0, rb.velocity.y);
            isMoving = false;
        }
    }

    private void LeftPressed()
    {
        if(rb.velocity.x <= speed * 0.2f){
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
            isMoving = true;
            isRight = false;
        }
    }

    private void LeftReleased()
    {
        if(isMoving && !isRight){
            rb.velocity = new Vector2(0, rb.velocity.y);
            isMoving = false;
        }
    }
    
    private void UpPressed()
    {
        if(IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    private void UpReleased()
    {
        if(rb.velocity.y > 0f){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f);
        }
    }

    private void DownPressed()
    {

    }

    private void DownReleased()
    {

    }
}
