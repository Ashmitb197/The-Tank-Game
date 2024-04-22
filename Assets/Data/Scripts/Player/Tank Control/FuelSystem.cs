using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    public TankController controllerScriptRef;
    public const float MaxFuel = 100;
    public float currentFuel;
    public float lastTime;
    public float UpdatedTime;

    public Slider fuelBar;

    public float maxAccelerationPower;

    public bool EmergencyFuel;


    FuelSystem()
    {
        
        currentFuel = MaxFuel;

    }
    // Start is called before the first frame update
    void Start()
    {
        EmergencyFuel = true;
        controllerScriptRef = this.GetComponent<TankController>();
        fuelBar = GameObject.Find("HUD").transform.Find("FuelBar").transform.Find("Slider").GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fuelBar != null)
        {
            

            UpdateFuelBar();
            
        }

        decreaseFuel();
        EmptyFuel();

        UpdateFuelBar();

        ActivateEmergencyFuel();
    }

    public void EmptyFuel()
    {
        controllerScriptRef.accelerationPower = (currentFuel<1)?0:controllerScriptRef.maxAccelerationPower;
    }

    public void decreaseFuel()
    {
        if(controllerScriptRef.GetVehicleSpeed() > 1)
            {
                if(Time.time > lastTime +  UpdatedTime && currentFuel > 0)
                {
                    currentFuel -= 0.1f;

                    lastTime = Time.time;
                }
            }

    }

    public void refuel(float fuel)
    {
        currentFuel += fuel;

        if(currentFuel >= 100)
            currentFuel = 100;

    }
    public void UpdateFuelBar()
    {
        fuelBar.maxValue = MaxFuel;
        fuelBar.value = currentFuel;

    }

    void ActivateEmergencyFuel()
    {
        if(Input.GetButtonDown("Emergency Fuel") && EmergencyFuel)
        {
            currentFuel = MaxFuel;

            EmergencyFuel = !EmergencyFuel;
        }


    }

    
}
