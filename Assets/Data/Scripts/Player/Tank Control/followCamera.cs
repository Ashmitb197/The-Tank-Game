using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class followCamera : MonoBehaviour
{
    public GameObject target;
    public float sensitivity;
    public GameObject mainCamera;

    [Range(0,179)]public float cameraFOV;

    public Vector3 cameraAdditionalOffset;

    public GameObject muzzleRef;

    public float mouseX;
    float mouseY;

    public CameraShaker cameraShaker;
    public OptionGraphics pauseMenuRef;

    followCamera()
    {
        sensitivity = 50;
        cameraFOV = 60;
        cameraAdditionalOffset = new Vector3(0,0.55f,-2.5f);

        
    }

    


    void Start()
    {
        mainCamera = transform.Find("Camera").gameObject;
        cameraShaker = mainCamera.GetComponent<CameraShaker>();
        target = GameObject.Find("Player_Tank");

        this.transform.rotation = Quaternion.Euler(0,90,0);

        muzzleRef = target.transform.Find("Turret").transform.Find("Muzzle").gameObject;
        //pauseMenuRef = GameObject.Find("PauseMenu").transform.Find("PauseOption").GetComponent<OptionGraphics>();

        
    }

    void FixedUpdate()
    {
        this.sensitivity = pauseMenuRef.Sensitivity;
        followTarget();
        cameraPlacement();
        fieldOfViewSettings();
    }

    void followTarget()
    {
        
        transform.position = target.transform.position;
    }

    void cameraPlacement()
    {
        if(Input.GetAxisRaw("Fire2") > 0)
        {
            
            cameraShaker.RestPositionOffset = Vector3.zero + muzzleRef.transform.localPosition;
            //cameraFOV = 40;
            mouseY = Mathf.Clamp(mouseY, 0, 10);


        }
        else
        {
            //cameraShaker.RestPositionOffset = Vector3.zero + muzzleRef.transform.localPosition;
            cameraShaker.RestPositionOffset = Vector3.zero + cameraAdditionalOffset;
            //cameraFOV = 60;
            mouseY = Mathf.Clamp(mouseY, -10, 10);
        }

        float horizontalAxis = Input.GetAxis("Mouse X");
        float verticalAxis = Input.GetAxis("Mouse Y");

        mouseX += horizontalAxis * sensitivity;
        mouseY += verticalAxis * sensitivity;   

        float currentMouseY = pauseMenuRef.isMouseInverted?-mouseY:mouseY;

        // mouseY = Mathf.Clamp(mouseY, -10, 25);
        transform.localRotation = Quaternion.Euler(currentMouseY,mouseX, 0);

    }

    

    void fieldOfViewSettings()
    {
        
        // TO DO --> smooth camera FOV update
        mainCamera.GetComponent<Camera>().fieldOfView = cameraFOV;
    }



}
