using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSensor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.transform.gameObject.GetComponent<Rigidbody>())
        {
            coll.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(coll.transform.gameObject.GetComponent<Rigidbody>().mass*100.0f, this.transform.position, 40.0f, coll.transform.gameObject.GetComponent<Rigidbody>().mass*1000.0f);
            Destroy(transform.parent.gameObject);
        }
    }
}
