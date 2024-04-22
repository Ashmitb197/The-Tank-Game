using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            coll.transform.gameObject.GetComponent<TankHealth>().increaseHealth(Random.Range(25,30));
            coll.transform.gameObject.GetComponent<FuelSystem>().refuel(100);
            coll.transform.gameObject.GetComponent<FuelSystem>().EmergencyFuel = true;

            coll.transform.gameObject.GetComponent<ammoMech>().currentMAG = coll.transform.gameObject.GetComponent<ammoMech>().maxMAG;


        }
    }
}
