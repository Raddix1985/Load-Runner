using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    // Varaibles
    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

    void Start()
    {
        // Set camera start location and player follow
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    
    void LateUpdate()
    {
        // Fix camera position while following
        moveVector = lookAt.position + startOffset;

        // x
        moveVector.x = 0;

        // y
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        // Animation at start of game
        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
           
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }

        

    }
}
