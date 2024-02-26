using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    public GameObject target;
    [Range(0,100)] public int sensitivity;
    public GameObject mainCamera;

    [Range(0,179)]public float cameraFOV;

    public Vector3 cameraAdditionalOffset;

    public GameObject muzzleRef;

    public float mouseX;
    float mouseY;

    followCamera()
    {
        sensitivity = 50;
        cameraFOV = 60;
        cameraAdditionalOffset = new Vector3(0,0.55f,-2.5f);
        
    }

    void Start()
    {
        mainCamera = transform.Find("Camera").gameObject;
        target = GameObject.Find("Player_Tank");
        muzzleRef = target.transform.Find("Turret").transform.Find("Muzzle").gameObject;
        
    }

    void FixedUpdate()
    {
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

            mainCamera.transform.localPosition = muzzleRef.transform.localPosition;
            cameraFOV = 40;
            mouseY = Mathf.Clamp(mouseY, 0, 0);


        }
        else
        {
            mainCamera.transform.localPosition = Vector3.zero + cameraAdditionalOffset;
            cameraFOV = 60;
            mouseY = Mathf.Clamp(mouseY, -10, 25);
        }

        float horizontalAxis = Input.GetAxis("Mouse X");
        float verticalAxis = Input.GetAxis("Mouse Y");

        mouseX += horizontalAxis * sensitivity;
        mouseY += verticalAxis * sensitivity;

        // mouseY = Mathf.Clamp(mouseY, -10, 25);
        transform.localRotation = Quaternion.Euler(mouseY,mouseX, 0);

    }

    

    void fieldOfViewSettings()
    {
        
        // TO DO --> smooth camera FOV update
        mainCamera.GetComponent<Camera>().fieldOfView = cameraFOV;
    }



}
