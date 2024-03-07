using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class EnemyTankMovement : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public List<WheelCollider> powerTyres;
    public List<WheelCollider> steerTyres;

    [Header("Wheel Meshes")]
    public List<GameObject> powerTyresMesh;
    public List<GameObject> steerTyresMesh;
    public GameObject target;
    public GameObject bullet;

    public GameObject turretRef;

    public GameObject muzzlePointRefrence;
    float lastTime = 0;
    float updateTime = 3.0f;

    [Header("Vehichle Working")]
    public float forwardAmount;
    public float turnAmount;

    public float maxSteerAngle = 45;
    public float VechicleAcceleration;

    // Start is called before the first frame update


    void Start()
    {
        

        //Colliders
        powerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RL_Wheel").gameObject.GetComponent<WheelCollider>();
        powerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RR_Wheel").gameObject.GetComponent<WheelCollider>();

        steerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FL_Wheel").gameObject.GetComponent<WheelCollider>();
        steerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FR_Wheel").gameObject.GetComponent<WheelCollider>();

        //For Wheel Mesh
        powerTyresMesh[0] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("RL_Wheel").gameObject;
        powerTyresMesh[1] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("RR_Wheel").gameObject;    

        steerTyresMesh[0] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("FL_Wheel").gameObject;
        steerTyresMesh[1] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("FR_Wheel").gameObject;

        turretRef = this.transform.Find("Turret").gameObject;
        target = GameObject.Find("Player_Tank");
        muzzlePointRefrence = turretRef.transform.Find("Muzzle").gameObject.transform.Find("MuzzlePoint").gameObject;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Accelerate();
        steerHandling();
        ApplyWheelMeshUpdate();
        UpdateLookRotation();
    }
    public void setInputs(float forwardAmount, float turnAmount)
    {
        this.forwardAmount = forwardAmount;
        this.turnAmount = turnAmount;
    }
    public void Accelerate()
    {

        for(int i = 0; i< powerTyres.Count; i++)
        {
            powerTyres[i].motorTorque = forwardAmount * VechicleAcceleration;
            
        }
    }

    void steerHandling()
    {
        for(int i = 0; i<steerTyres.Count; i++)
        {
            steerTyres[i].steerAngle = turnAmount*maxSteerAngle;
        }
    }

    public void brakeHandling()
    {
        
        for(int i = 0; i<powerTyres.Count; i++)
        {
            powerTyres[i].brakeTorque = 5000;
        }
      
    }


    //Testing Method
    void UpdateLookRotation()
    {
        
        if(Vector3.Distance(target.transform.position, this.transform.position)<10)
        {
            turretRef.transform.LookAt(target.transform);
            
            shootBullet();
        }
    }

    void shootBullet()
    {
       
        if(Time.time >lastTime + updateTime )
        {
            Instantiate(bullet, muzzlePointRefrence.transform.position, turretRef.transform.rotation);
            lastTime = Time.time;         
        }
    }

     void ApplyWheelMeshUpdate()
    {
        for(int i = 0; i<steerTyres.Count; i++)
        {
            UpdateWheelMesh(steerTyres[i], steerTyresMesh[i]);
        }

        for(int i = 0; i<powerTyres.Count; i++)
        {
            UpdateWheelMesh(powerTyres[i], powerTyresMesh[i]);
        }

    }

    void UpdateWheelMesh(WheelCollider collider, GameObject wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);

        wheelMesh.transform.position = pos;
        wheelMesh.transform.rotation = rot;
    }
}
