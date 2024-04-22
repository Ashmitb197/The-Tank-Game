using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public bool IsCountedAsDead;

    //For Debugging purpose
    public Rigidbody rigidBody;



    BuildingHealth()
    {
        currentHealth = maxHealth;
        
        
    }

    void Start()
    {
        IsCountedAsDead = false;
        // rigidBody = this.GetComponent<Rigidbody>();
        // rigidBody.isKinematic = true;
    }
    public void decreaseHealth(float decreaseHealth)
    {
        currentHealth -= decreaseHealth;

        if(currentHealth < 0)
        {
            currentHealth = 0;
            //Die();
        
        }

    }

    public bool Die()
    {
        
        // this.GetComponent<Collider>().isTrigger = true;
        // rigidBody.isKinematic = false;
        if(currentHealth == 0)
        {
            this.GetComponent<Animator>().SetBool("IsDead", true);
            return true;
        
        }
        
        else 
            return false;
    }

    // void FixedUpdate()
    // {
    //     if(currentHealth <= 0)
    //     {
    //         this.transform.Translate(0, 0.05f, 0);
    //     }
    // }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider)
        {
            decreaseHealth(Random.Range(3,5));
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Shell")
        {
            decreaseHealth(Random.Range(10,15));
        }
    }
}
