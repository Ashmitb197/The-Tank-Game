using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretRotation : MonoBehaviour
{
    [Header("GameObject Refrences")]
    public GameObject turretRefrence;
    public GameObject muzzlePointRefrence;
    public GameObject particleSystemRefrence;
    public GameObject[] LaunchersRefrence;

    public followCamera cameraScript;

    
    [Header("Input Axis")]
    public float mouseX;
    public float mouseY;
    public Vector2 turretXClamp;
    public Vector2 turretYClamp;
    [Range(0,100)] public int Sensitivity = 1;

    [Header("Explosion Related Values")]
    [Range(20, 100)]public float explosionPower = 5;
    [Range(0,5)]public float impactRadius = 2;
    [Range(0,1)] public float upwardForce = 0.5f;


    // [Header("Script Refrences")]
    // public BulletSpawnScript bulletSpawnScript;


    float rotationX;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = GameObject.Find("CameraPivotPoint").GetComponent<followCamera>();
        // bulletSpawnScript = GameObject.Find("Launchers").GetComponent<BulletSpawnScript>();
        turretRefrence = transform.Find("Turret").gameObject;
        //muzzlePointRefrence = turretRefrence.transform.Find("rotator").gameObject.transform.Find("mussle").gameObject.transform.Find("musclepoint").gameObject;
        particleSystemRefrence = GameObject.Find("Particle System");
    }

    // Update is called once per frame
    void Update()
    {
        //shootRay();
        mouseMovement();
        
    }

    void mouseMovement()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotationY += mouseX * cameraScript.sensitivity;
        rotationX -= mouseY * cameraScript.sensitivity;

        // Clamp the pitch angle to avoid over-rotation.
        //rotationX = Mathf.Clamp(rotationX, turretXClamp.x, turretXClamp.y);
        //rotationY = Mathf.Clamp(rotationY, turretYClamp.x, turretYClamp.y);

        // Apply the rotation to the GameObject.
        turretRefrence.transform.rotation = Quaternion.Euler(0, cameraScript.transform.rotation.eulerAngles.y, 0);
    }

    void shootRay()
    {
        RaycastHit hit;

        bool shootRay = Physics.Raycast(muzzlePointRefrence.transform.position, muzzlePointRefrence.transform.forward, out hit, Mathf.Infinity);
        
       Debug.DrawRay(muzzlePointRefrence.transform.position, muzzlePointRefrence.transform.forward*Mathf.Infinity, shootRay ?Color.green : Color.red);
        if(shootRay)
        {
            Debug.Log("Did Hit: " + hit.collider.gameObject.name);

            if(hit.rigidbody != null && Input.GetButtonDown("Fire1"))
            {
                
                particleSystemRefrence.transform.position = hit.collider.gameObject.transform.position;
                //hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(, ForceMode.Impulse);
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*explosionPower + new Vector3(5 ,5,0), ForceMode.Impulse);
                particleSystemRefrence.GetComponent<ParticleSystem>().Play();
                //particleSystemRefrence.GetComponent<ParticleSystem>().Stop();

                // for(int i = 0; i<4; i++)
                // {
                //     LaunchersRefrence[i].GetComponent<BulletSpawnScript>().SetShouldShoot(true);
                // }
                //StartCoroutine(stopShooting());


            }

        }
    }

    // IEnumerator stopShooting()
    // {
    //     yield return new WaitForSeconds(10.0f);
    //     for(int i = 0; i<4; i++)
    //     {
    //         LaunchersRefrence[i].GetComponent<BulletSpawnScript>().SetShouldShoot(false);
    //     }
    // }

}
