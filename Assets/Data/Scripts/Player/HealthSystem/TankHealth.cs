using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;
    public float maxArmourHealth = 50;
    public float currentArmourHealth;

    public GameObject HealthUISystem;
    public Slider armourSlider;
    public Slider healthSlider;

    void Awake()
    {
        HealthUISystem = GameObject.Find("HealthSystem");
        armourSlider = HealthUISystem.transform.Find("ArmourBar").GetComponent<Slider>();
        healthSlider = HealthUISystem.transform.Find("HealthBar").GetComponent<Slider>();
    }
    void Start()
    {
        currentArmourHealth = maxArmourHealth;
        currentHealth = maxHealth;
    }

    void Update()
    {
        UpdateSliderValue(currentHealth, healthSlider, maxHealth);
        UpdateSliderValue(currentArmourHealth, armourSlider, maxArmourHealth);
    }

    public void increaseHealth(float health)
    {
        currentHealth += health;

        if(currentHealth >= 100)
            currentHealth = 100;
    }
    void decreaseArmourHealth(float health)
    {
        currentArmourHealth -= health;

        if(currentArmourHealth <= 0)
        {
            currentArmourHealth = 0;
            decreaseHealth(health);
        }
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
        Time.timeScale = 0;
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider)
        {
            decreaseArmourHealth(Random.Range(2,5));
        }
    }

    void OnTriggerEnter(Collider coll)
    {
            if(coll.GetComponent<Collider>().tag == "Bullet")
            {
                decreaseArmourHealth(Random.Range(10,15));
            }
            else if(coll.GetComponent<Collider>().tag == "Mine")
            {
                decreaseArmourHealth(100);
            }

    }

    void UpdateSliderValue(float value, Slider sliderType, float maxValue)
    {
        sliderType.maxValue = maxValue;
        sliderType.value = value;
    }
}
