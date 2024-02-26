using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    //For Debugging purpose
    public Rigidbody rigidBody;



    BuildingHealth()
    {
        currentHealth = maxHealth;
        
        
    }

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
    }
    public void decreaseHealth(float decreaseHealth)
    {
        currentHealth -= decreaseHealth;

        if(currentHealth < 0)
        {
            currentHealth = 0;
            Die();
        
        }

    }

    void Die()
    {
        this.GetComponent<Collider>().isTrigger = true;
        rigidBody.isKinematic = false;
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
}
