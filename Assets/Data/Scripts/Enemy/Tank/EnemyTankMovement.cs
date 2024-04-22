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
    public float turretRotationSpeed;

    public GameObject muzzlePointRefrence;
    float lastTime = 0;
    public float updateTime;

    [Header("Vehichle Settings")]
    public float forwardAmount;
    public float turnAmount;

    public float maxSteerAngle = 45;
    public float VechicleAcceleration;
    public bool canTurretRotate;


    [Header("Sounds")]
    public AudioSource reloadSound;
    public AudioSource shootSound;

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

        //turretRef = this.transform.Find("Turret").transform.Find("TurretHand").transform.Find("TurretRotor").gameObject;
        
        //muzzlePointRefrence = turretRef.transform.Find("MuzzlePoint").gameObject;


        // reloadSound = this.transform.Find("Reload").GetComponent<AudioSource>();
        // shootSound = this.transform.Find("Shoot").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = GameObject.FindWithTag("Player");
        if(target)
            UpdateLookRotation();
        Accelerate();
        steerHandling();
        ApplyWheelMeshUpdate();
        
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
            powerTyres[i].brakeTorque = 5000000;
        }
      
    }


    //Testing Method
    void UpdateLookRotation()
    {
        
        if((Vector3.Distance(target.transform.position, this.transform.position)<20) && canTurretRotate)
        {
            turretRef.transform.LookAt(target.transform);
            // Vector3 targetDirection = target.transform.position - turretRef.transform.position;
            // turretRef.transform.localRotation =  Quaternion.LookRotation(targetDirection);
            
            shootBullet();
        }
    }

    void shootBullet()
    {
       
        if(Time.time >lastTime + updateTime )
        {
            Instantiate(bullet, muzzlePointRefrence.transform.position, muzzlePointRefrence.transform.rotation);
            //StartCoroutine(playShootAndReloadSound());
            lastTime = Time.time;         
        }
    }

    // IEnumerator playShootAndReloadSound()
    // {
    //     shootSound.Play();
    //     yield return new WaitForSeconds(1.750f);
    //     reloadSound.Play();
    // }

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
