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

    void Start()
    {
        mainCamera = GameObject.Find("Camera");
        target = GameObject.Find("Player_Car");
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

        }
        else
        {
            mainCamera.transform.localPosition = Vector3.zero + cameraAdditionalOffset;
        }

        float horizontalAxis = Input.GetAxis("Mouse X");
        float verticalAxis = Input.GetAxis("Mouse Y");

        mouseX += horizontalAxis * sensitivity;
        mouseY += verticalAxis * sensitivity;

        mouseY = Mathf.Clamp(mouseY, -25, 25);
        transform.localRotation = Quaternion.Euler(0,mouseX, 0);

    }

    

    void fieldOfViewSettings()
    {
        
        // TO DO --> smooth camera FOV update
        mainCamera.GetComponent<Camera>().fieldOfView = cameraFOV;
    }



}
