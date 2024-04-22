using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{

    public List<Transform> shells;

    public int MaxLauncherAmmo;
    public GameObject LauncherAmmo;

    public GameObject childshell;


    public Vector3 targetLoc;
    // Start is called before the first frame update
    void Start()
    {

        shells[0] = this.transform.Find("missl.001").transform.Find("0");
        shells[1] = this.transform.Find("missl.001").transform.Find("1");
        shells[2] = this.transform.Find("missl.001").transform.Find("2");
        shells[3] = this.transform.Find("missl.001").transform.Find("3");
        shells[4] = this.transform.Find("missl.001").transform.Find("4");
        shells[5] = this.transform.Find("missl.001").transform.Find("5");

        
    }

    void FixedUpdate()
    {

    }


    public void spawnBulletInLauncher()
    {

        foreach(var launcherShell in shells)
        {
            
            childshell  = Instantiate(LauncherAmmo, launcherShell.position, launcherShell.rotation);

            childshell.transform.parent = launcherShell;

            
        }   
    }


    //Launcher Ammo Script
    // bool CheckandShootEnemyNearby()
    // {
    //     var colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity);

    //     foreach(var collider in colliders) {

    //         if(collider.CompareTag("Enemy"))
    //         {
    //             targetLoc = collider.transform.position;  
    //             return true;

    //         }
        
    //     }

    //     return false;
        
    // }
}
