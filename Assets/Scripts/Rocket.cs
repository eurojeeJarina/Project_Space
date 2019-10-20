using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    //[SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    //[SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelLoadSound;

   // [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem levelLoadParticles;


    

 /*   [SerializeField] Button leftButton;
    [SerializeField] Button RightButton;*/
   

    Rigidbody rigidBody;
    AudioSource audioSource;
    int currentSceneIndex;
    int maxSceneIndex = 2;


    enum State {Alive, Dying, Transcending};

    State state = State.Alive;

    public static bool alive = true;
    public static float fuelEnergy = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        fuelEnergy = 100f;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
/*      
        RightButton.GetComponent<Button>();
        leftButton.GetComponent<Button>();

        leftButton.onClick.AddListener(RespondToLeftButton);
        RightButton.onClick.AddListener(RespondToLeftButton);*/
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == State.Alive)
        {
            alive = true;
            CheckFuel();
            //RespondToThrustInput();
            RespondToRotateInput();
        }
        else {
            alive = false;
        }
    }
    void CheckFuel()
    {
        if(fuelEnergy <= 0)
        {
            StartDeathSequence();
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
            case "Fuel":
                print("Fuel");
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
        levelLoadParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay); // wait for 1 second before loading next scene
    }

    void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
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
        if (currentSceneIndex >= maxSceneIndex)
        {
            currentSceneIndex = 0;
            print(currentSceneIndex);
            SceneManager.LoadScene(currentSceneIndex); // todo allow more than 2 levels;
        }
        else {
            currentSceneIndex++;
            print(currentSceneIndex);
            SceneManager.LoadScene(currentSceneIndex);
        }
        
        
    }

   
    
    void LoadFirstLevel() 
    {
        SceneManager.LoadScene(currentSceneIndex); // todo allow more than 2 levels;
    }

    /*    private void RespondToThrustInput()
        {
            if (Input.GetButton("Fire1")) // can trust while rotating
            {
                ApplyThrust();
            }
            else {
                audioSource.Stop();
                mainEngineParticles.Stop();
            }
        }*/

    /*    void ApplyThrust()
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // TODO: try & comeback solve the Time.deltaTime; problem.

            if (!audioSource.isPlaying) // so it doesn't layer
            {
                audioSource.PlayOneShot(mainEngineSound);
            }
            mainEngineParticles.Play();
        }*/

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
