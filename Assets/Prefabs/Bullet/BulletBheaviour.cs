using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class BulletBheaviour : MonoBehaviour
{

    public GameObject blastPS;

    public float bulletSpeed = 5000;

    public float magnitude;
    public float roughness;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward*bulletSpeed);
        //StartCoroutine(die());
        //die();
        //this.transform.Translate(this.transform.forward*1);
    }


    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag != "Bullet")
        {
            Instantiate(blastPS, transform.position, transform.rotation);
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, .1f, 1f);
            die();
        }
    }

    void die()
    {
        //yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
