using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RightButtonEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject rocketPlayer;
    [SerializeField] float rcsThrust = 100f;

    bool fingerOn = false;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fingerOn)
        {
            rotationSpeed = rcsThrust * Time.deltaTime;
            rocketPlayer.transform.Rotate(-Vector3.forward * rotationSpeed);
            rocketPlayer.GetComponent<Rigidbody>().freezeRotation = false; // resume physics control of rotation
        }
        else
        {
            rocketPlayer.GetComponent<Rigidbody>().freezeRotation = true; // take manual control of rotation
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        fingerOn = true;

        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        fingerOn = false;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {


    }
}
