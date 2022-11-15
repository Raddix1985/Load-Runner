using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // variables
    private CharacterController controller;
    private Vector3 moveVector;

    private float jumpForce = 4.0f;
    private float speed = 3.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    

    private float animationDuration = 3.0f;

    private bool isDead = false;

    void Start()
    {
        // Get player object
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        if (isDead)
            return;

        // Set Player start animation position
       if(Time.time < animationDuration)
        {
           controller.Move(Vector3.forward * speed * Time.deltaTime);
           return;
        }

           moveVector = Vector3.zero;
        // Check if player is grounded and set gravity
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

      
        
        // Player movement

        // x - left and right
        moveVector.x = Input.GetAxisRaw("Horizontal");

        // y = up and down
        moveVector.y = verticalVelocity;

        //z = forward and backward
        moveVector.z = speed;

        controller.Move(moveVector * speed * Time.deltaTime);
    }

  

    public void SetSpeed(float modifier)
    {
        speed = 3.0f + modifier;
    }

    // Being called everytime player hits an obstacle
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       if (hit.point.z > transform.position.z + controller.radius + 0.1f && hit.gameObject.tag == "Obstacle")
        {
            Death();
            Debug.Log("Death");
        }
    }

    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}
