using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    public TankController controllerScriptRef;
    public const float MaxFuel =100;
    public float currentFuel;
    public float lastTime;
    public float UpdatedTime;

    public Slider fuelBar;

    public float maxAccelerationPower;



    FuelSystem()
    {
        
        currentFuel = MaxFuel;

    }
    // Start is called before the first frame update
    void Start()
    {
        maxAccelerationPower = 400;
        controllerScriptRef = this.GetComponent<TankController>();
        fuelBar = GameObject.Find("Canvas").transform.Find("FuelBar").gameObject.GetComponent<Slider>();
        
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
    }

    public void EmptyFuel()
    {
        controllerScriptRef.accelerationPower = (currentFuel<1)?0:maxAccelerationPower;
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
    public void UpdateFuelBar()
    {
        fuelBar.value = currentFuel;

    }
}
