 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    
    [Header("Renderer/Trail Effects")]
    public List<TrailRenderer> tyreMarks;


    [Header("Wheel Colliders")]
    public List<WheelCollider> powerTyres;
    public List<WheelCollider> steerTyres;

    [Header("Wheel Meshes")]
    public List<GameObject> powerTyresMesh;
    public List<GameObject> steerTyresMesh;

    public float accelerateInput;
    public float steerInput;
    public bool brakeInput;

    [Header("Vehicle Details")]
    public float accelerationPower = 400;
    public float maxAccelerationPower = 400000;
    public float maxSteerAngle = 30;
    public float brakeForce = 160000;
    public float currentSpeed;
    public float jumpForce;
    public float maxSpeed;

    public Vector3 COM;

    [Header("Health Info")]
    public TankHealth tankHealthRef;


    public bool isOnGround;

    void Start()
    {

        tankHealthRef = this.GetComponent<TankHealth>();

        COM = transform.Find("COM").localPosition;

        this.GetComponent<Rigidbody>().centerOfMass = COM;

        tyreMarks[0] = transform.Find("Wheels").transform.Find("SkidMarks").transform.Find("LeftSkid").gameObject.GetComponent<TrailRenderer>();
        tyreMarks[1] = transform.Find("Wheels").transform.Find("SkidMarks").transform.Find("RightSkid").gameObject.GetComponent<TrailRenderer>();

        //For Wheel Colliders
        powerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RL_Wheel").gameObject.GetComponent<WheelCollider>();
        powerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RR_Wheel").gameObject.GetComponent<WheelCollider>();

        steerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FL_Wheel").gameObject.GetComponent<WheelCollider>();
        steerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FR_Wheel").gameObject.GetComponent<WheelCollider>();

        //For Wheel Mesh
        powerTyresMesh[0] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("RL_Wheel").gameObject;
        powerTyresMesh[1] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("RR_Wheel").gameObject;    

        steerTyresMesh[0] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("FL_Wheel").gameObject;
        steerTyresMesh[1] = transform.Find("Wheels").transform.Find("Meshes").transform.Find("FR_Wheel").gameObject;





    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void FixedUpdate()
    {

        gettingInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        //PlayerRef.SetActive(false);
        Accelerate();
        steerHandling();
        brakeHandling();
        ApplyWheelMeshUpdate();  

        checkDrift();
        //DebugPrintCarSpeed();
        IsTankGrounded();

        Jump();
    }

    void gettingInput(float steerInput, float accelerateInput)
    {
        this.accelerateInput = accelerateInput;
        this.steerInput = steerInput;
        brakeInput = Input.GetKey(KeyCode.Space);
    }

    void Accelerate()
    {
        //float currentaccelerationPower = (GetVehicleSpeed() < maxSpeed)? accelerationPower : 0;
        for(int i = 0; i< powerTyres.Count; i++)
        {
            powerTyres[i].motorTorque = accelerateInput* (accelerationPower);
            
        }
        
    }

    void Jump()
    {
        if(Input.GetKeyDown("j") && IsTankGrounded())
            this.GetComponent<Rigidbody>().AddForce(transform.up *(jumpForce));
    }

    void steerHandling()
    {
        for(int i = 0; i<steerTyres.Count; i++)
        {
            steerTyres[i].steerAngle = steerInput*maxSteerAngle;
        }
    }

    void brakeHandling()
    {
        float currentbrakeForce = brakeInput ? brakeForce : 0;
        
        for(int i = 0; i<powerTyres.Count; i++)
        {
            powerTyres[i].brakeTorque = currentbrakeForce;
        }
      
    }

    void DebugPrintCarSpeed() //Debug Function
    {
        Debug.Log(GetVehicleSpeed());
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


    public float GetVehicleSpeed()
    {
        currentSpeed = ((2*Mathf.PI*(powerTyres[1].radius))*(powerTyres[1].rpm/(60)));
        return currentSpeed;
    }

    // public float GetSpeedRatio()
    // {
    //     var power = Mathf.Clamp(accelerateInput,0.5f, 1f);
    //     return GetVehicleSpeed()*power/maxSpeed;
    // }

    //Trail Renderer For Tire Skid

    private void checkDrift()
    {
        if(this.GetComponent<Rigidbody>().velocity.magnitude > 0 && brakeInput && IsTankGrounded())
        {
            StartEmitting();
        }
        else
        {
            StopEmitting();
        }
    }

    private void StartEmitting()
    {
        foreach (TrailRenderer i in tyreMarks)
        {
            i.emitting =true;
        }
    }

    private void StopEmitting()
    {
        foreach (TrailRenderer i in tyreMarks)
        {
            i.emitting =false;
        }
    }


    //Particle System for tire dust



    bool IsTankGrounded()
    {

        isOnGround = Physics.Raycast(transform.position, -this.transform.up, 2);
        Debug.DrawRay(transform.position, transform.up * -2, Color.white);

        return isOnGround;
    }

}
