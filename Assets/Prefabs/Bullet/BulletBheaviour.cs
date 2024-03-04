using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBheaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward*1000);
        StartCoroutine(die());
    }


    IEnumerator die()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
