using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigggerPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] startPoints;
    public Transform startPoint;
    public Transform endPoint;
    public GameObject Enemy_Tank;

    GameObject spawnedTank;

    public bool hasSpawned;

    public float time;


    void Start()
    {
        hasSpawned = false;
        startPoints = new Transform[2];
        startPoints[0] = transform.Find("StartPoint1");
        startPoints[1] = transform.Find("StartPoint2");
        endPoint = transform.Find("EndPoint");

        
        
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

            spawnedTank = SpawnTank();
            // if(!hasSpawned)
            // {
            //     spawnedTank = SpawnTank();
            //     hasSpawned = !hasSpawned;
            // }
        }

            
    }

    GameObject SpawnTank()
    {
        startPoint = startPoints[Random.Range(0,2)];
        GameObject spawnedTank = Instantiate(Enemy_Tank, startPoint.position, startPoint.rotation);
        return spawnedTank;
    }


}
