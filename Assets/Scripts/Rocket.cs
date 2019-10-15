using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;

    enum State {Alive, Dying, Transcending};

    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //todo somewhere stop sound on death
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive){return;} // break the execution 

        switch (collision.gameObject.tag) // check tags on collision
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f); // wait for 1 second before loading next scene
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 2f);
                break;
        }
    }

    void LoadNextScene()
    {
/*        switch (state)
        {
            case State.Transcending:
                print("Transcending State");
                break;
            case State.Dying:
                print("Dying State");
                break;
            default:
                print("No state");
                break;
        }*/

        SceneManager.LoadScene(1); // todo allow more than 2 levels;
    }
    
    void LoadFirstLevel() 
    {
        SceneManager.LoadScene(0); // todo allow more than 2 levels;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // can trust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        
        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) // rotate left
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D)) // rotate right
        {   
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }
}
