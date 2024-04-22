using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherAmmoInfo : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public Animator anim;
    public Vector3 TargetLocation;

    public GameObject blastPS;

    public bool isTargetNearby;
    public bool startFollowingTarget;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        startFollowingTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsTargetNearby", CheckandShootEnemyNearby());
        directToWardsTarget();

    }

    public void getTargetLocation(Vector3 TargetLocation)
    {
        this.TargetLocation = TargetLocation;
    }
    public void directToWardsTarget()
    {
        if(startFollowingTarget)
        {
            this.transform.parent = null;
            this.GetComponent<Animator>().enabled = false;
            this.transform.position = Vector3.MoveTowards(transform.position, TargetLocation, speed*Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(TargetLocation - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag != "LauncherShell")
        {

            

            Instantiate(blastPS, transform.position, transform.rotation);
            StartCoroutine(die());
        }
        if(coll.tag == "Enemy")
        {
            coll.gameObject.GetComponent<EnemyTankHealth>().decreaseHealth(Random.Range(50,80));
        }

    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    bool CheckandShootEnemyNearby()
    {
        var colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity);

        foreach(var collider in colliders) {

            if(collider.CompareTag("Enemy"))
            {
                TargetLocation = collider.transform.position;  
                return true;

            }
        
        }

        return false;
        
    }
}
