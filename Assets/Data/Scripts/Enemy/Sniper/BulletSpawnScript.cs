using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{

    public GameObject target;
    public float Range = 40;

    public bool playerDetected;


    void Awake()
    {
        target = GameObject.Find("Player_Tank");
        playerDetected = false;
    }


    void Update()
    {
        //Debug.Log(Vector3.Distance( target.transform.position, this.transform.position));
        if(Vector3.Distance(target.transform.position, this.transform.position) < Range)
        {
            playerDetected = true;
        }
        else{
            playerDetected = false;
        }

        UpdateRotation();
    }

    void UpdateRotation()
    {
        if(!playerDetected)
           UpdateRotationOnPlayerNotDetected();

        if(playerDetected)
            UpdateRotationOnPlayerDetection();
    }

    void UpdateRotationOnPlayerNotDetected()
    {
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0,this.transform.rotation.y,0), 0.01f);
        this.transform.Rotate(Vector3.Lerp(new Vector3(0,0,0), new Vector3(0,90,0), 0.01f));
    }
    void UpdateRotationOnPlayerDetection()
    {
        this.transform.LookAt(target.transform, Vector3.up);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
