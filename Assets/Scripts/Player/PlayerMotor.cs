using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 0.2f;

    // variables
    private CharacterController controller;
    

    private float jumpForce = 4.0f;
    private float verticalVelocity;
    private float gravity = 12.0f;
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right

    // speed modifier
    private float speed = 3.0f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 10.0f;
    private float speedIncreaseAmount = 0.1f;


    private float animationDuration = 3.0f;

    private bool isDead = false;
    private bool isRunning;

    public static int numOfPower;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    private AudioSource playerAudio;
    public AudioClip powerSound;

    

    void Start()
    {
        // Get player object
        controller = GetComponent<CharacterController>();
        numOfPower = 0;
        playerAudio = GetComponent<AudioSource>();
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

           
        // Check if player is grounded and set gravity
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // get inputs on which lane we should be
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLane(false);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveLane(true);

        // calculate where we should be in future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        // Player movement and move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        moveVector.y = -0.1f;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);

        // rotate player to where he is going
        Vector3 dir = controller.velocity;
        if(dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
        
    }

    // Lane movement
    private void MoveLane(bool goingright)
    {
        desiredLane += (goingright) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    // Increase speed
    public void SetSpeed(float modifier)
    {
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
        }
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
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Power")
        {
            playerAudio.PlayOneShot(powerSound, 1.0f);
        }
    }

    //Score on death
    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
 playerAudio.PlayOneShot(crashSound, 1.0f);
    }
}
