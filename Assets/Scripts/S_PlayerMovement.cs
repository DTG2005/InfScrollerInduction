using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float ForwardForce = 2000f;
    public float InputSensitivity = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, ForwardForce*Time.deltaTime);

        if(Input.GetKey("d"))
        {
            rb.AddForce(InputSensitivity * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-InputSensitivity * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }
    
}
