using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private float rotateSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( 0, rotateSpeed, 0 * Time.deltaTime);
    }
     // trigger collection of power 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerMotor.numOfPower += 1;
            Debug.Log("Power:" + PlayerMotor.numOfPower);
            Destroy(gameObject);
        }

    }
}
