using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can trust while rotating
        {
            print("Space Pressed");
        }
        if (Input.GetKey(KeyCode.A)) // rotate left
        {
            print("Rotating Left");
        } 
        else if (Input.GetKey(KeyCode.D)) // rotate right
        {
            print("Rotating Right");
        }
    }
}
