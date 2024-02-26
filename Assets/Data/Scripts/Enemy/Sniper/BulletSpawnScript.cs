using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{

    public Transform target;
    public float Range = 40;


    void Start()
    {
        target = transform.Find("Player_Tank");
    }


    void Update()
    {

        if(Vector3.Distance(target.position, this.transform.position) < Range)
        {
            Vector3 dir = target.position - this.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;

            this.transform.rotation = Quaternion.Euler(0,rotation.y,0);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
