using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ThrustButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject rocketPlayer;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] float fuelCost = 5f;


    AudioSource audioSource;
    bool fingerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        fuelCost = fuelCost * Time.deltaTime;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fingerOn && Rocket.alive)
        {

            rocketPlayer.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            Rocket.fuelEnergy = Rocket.fuelEnergy - fuelCost;

        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        fingerOn = true;
        
        mainThrustParticle.Play();
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        fingerOn = false;
        mainThrustParticle.Stop();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        fingerOn = false;
    }


}
