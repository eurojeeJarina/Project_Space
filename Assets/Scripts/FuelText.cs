using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowFuel();
    }

    void ShowFuel()
    {
        
        GetComponent<Text>().text = Rocket.fuelEnergy.ToString("f0") + "%";
    }
}
