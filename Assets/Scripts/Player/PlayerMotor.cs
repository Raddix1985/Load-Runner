using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;

    private float speed = 3.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    private float animationDuration = 3.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
       if(Time.time < animationDuration)
        {
           controller.Move(Vector3.forward * speed * Time.deltaTime);
           return;
        }

           moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        // x - left and right
        moveVector.x = Input.GetAxisRaw("Horizontal");

        // y = up and down
        moveVector.y = verticalVelocity;

        //z = forward and backward
        moveVector.z = speed;

        controller.Move(moveVector * speed * Time.deltaTime);
    }
}
