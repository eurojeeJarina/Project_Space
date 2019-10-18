using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelLoadSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem levelLoadParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending};

    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
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
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelLoadSound);
        Invoke("LoadNextScene", 1f); // wait for 1 second before loading next scene
    }

    void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        Invoke("LoadFirstLevel", 2f);
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

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can trust while rotating
        {
            ApplyThrust();
        }
        else {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);

        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(mainEngineSound);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
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
