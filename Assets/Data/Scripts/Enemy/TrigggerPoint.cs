using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum SelectedVehicle
{
    Tank,
    Car,
};
public class TrigggerPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Transform> startPoints;
    public List<GameObject> Vehicles;
    
    
    public bool SpawnAtAllPoints;
    public bool SpawnBothVehicle;

    public SelectedVehicle selectedVehicle;

    GameObject spawnedTank;

    public bool hasSpawned;

    public float time;


    void Start()
    {
        hasSpawned = false;
        
    }

    void FixedUpdate()
    {
        
        // if(spawnedTank)
        //     EnemyTankMovement();

        //if(spawnedTank.transform.position.z != endPoint.position.z) spawnedTank.transform.Translate();

    }

    // void EnemyTankMovement()
    // {
    //     Vector3 dirToEndPoint = Vector3.Normalize( spawnedTank.transform.position-endPoint.position);
    //     float direction = Vector3.Dot(endPoint.forward, dirToEndPoint);

    //     Debug.Log(direction);

    //     EnemyTankMovement ETankScript = spawnedTank.GetComponent<EnemyTankMovement>();

    //     if(direction<-0.05f || direction >0.05f)
    //     {
    //         ETankScript.Accelerate(-direction);
    //     }
    //     else
    //     {  
    //         ETankScript.brakeHandling();
        
    //     }
    // }
    void OnTriggerEnter(Collider coll)
    {
        

        if(coll.tag == "Player")
        {
            Debug.Log("Player Coll");

            SpawnTank();
            // if(!hasSpawned)
            // {
            //     spawnedTank = SpawnTank();
            //     hasSpawned = !hasSpawned;
            // }
            Destroy(gameObject);
        }

            
    }

    void SpawnTank()
    {
        if(!SpawnAtAllPoints)
        {
            //if spawn both vehicles = false;

            Transform startPoint = startPoints[Random.Range(0,(startPoints.Count-1))];

            if(!SpawnBothVehicle)
            {
                Instantiate(Vehicles[(int)selectedVehicle], startPoint.position, startPoint.rotation);
            }
            else
            {
                Instantiate(Vehicles[Random.Range(0,(Vehicles.Count-1))], startPoint.position, startPoint.rotation);
            }
        }
        else
        {
            for(int i = 0; i<startPoints.Count; i++)
            {
                //if spawn both vehicles = false
                if(!SpawnBothVehicle)
                {
                    Instantiate(Vehicles[(int)selectedVehicle], startPoints[i].position, startPoints[i].rotation);
                }
                else
                {
                    Instantiate(Vehicles[Random.Range(0,(Vehicles.Count-1))], startPoints[i].position, startPoints[i].rotation);
                }
            }
        }
    }



}
