using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateSpeed = Random.Range(0.5f, 1.5f);
        transform.Rotate( 0, rotateSpeed, 0 * Time.deltaTime);
    }
     // trigger collection of power 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerMotor.numOfPower += 1;
            Destroy(gameObject);
        }

    }
}
