using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 2.4f;
    private const float TURN_SPEED = 35.0f;

    // variables
    private CharacterController controller;
    private Animator anim;

    public static int numOfPower;

    private float jumpForce = 6.0f;
    private float verticalVelocity;
    private float gravity = -10.0f;
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right

    // speed modifier
    private float speed = 6.0f;



    private float animationDuration = 3.0f;

    private bool isDead = false;
    



    public AudioClip crashSound;
    public AudioClip jumpSound;
    private AudioSource playerAudio;
    public AudioClip powerSound;



    void Start()
    {
        GetComponent<Animator>();
        
        // Get player object
        controller = GetComponent<CharacterController>();
        numOfPower = 0;
        playerAudio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        bool isGrounded = controller.isGrounded;

        verticalVelocity += gravity * Time.deltaTime;

            if (MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                Jump();
                Debug.Log("grounded" + !controller.isGrounded);
                anim.SetTrigger("Jump_trig");

            }

          
       
        
        if (isDead)
            return;

        // Set Player start animation position
       if(Time.time < animationDuration)
        {
           controller.Move(Vector3.forward * speed * Time.deltaTime);
           return;
        }

           
    

        // get inputs on which lane we should be
        if (MobileInput.Instance.SwipeRight || Input.GetKeyDown(KeyCode.A))
            MoveLane(false);
        if (MobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.D))
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
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);

        // rotate player to where he is going
        Vector3 dir = controller.velocity;
       
        if (dir != Vector3.zero)
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
        speed = 6.0f + modifier;
    }

    // Being called everytime player hits an obstacle
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       if (hit.point.z > transform.position.z + 0.1f && hit.gameObject.tag == "Obstacle")
        {
           Death();
           FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }
    // Jump method
    private void Jump()
    {
        
        {
            
            verticalVelocity = jumpForce;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
       
    }

    // Collectable method
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
        //anim.SetBool("isDead", true);
        GetComponent<Score>().OnDeath();
        FindObjectOfType<AudioManager>().Stop("MainTheme");
        playerAudio.PlayOneShot(crashSound, 1.0f);
    }
}
