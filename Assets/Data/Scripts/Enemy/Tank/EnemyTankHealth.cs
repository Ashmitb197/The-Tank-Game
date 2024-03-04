using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;


    
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
    }

    void decreaseHealth(float health)
    {
        currentHealth -= health;
        
        if(currentHealth<=0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider)
        {
            if(coll.collider.tag == "Bullet")
            {
                decreaseHealth(Random.Range(10,15));
            }
            else{

                decreaseHealth(Random.Range(2,5));
            }
        }
    }

    
}
