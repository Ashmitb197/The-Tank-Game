using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;

    public GameObject healthSlider;

    public Transform cameraPivotPoint;


    [Header("Death Related Properties")]

    public EnemyTankMovement _enemyTankMovement;
    public ETankAIDriver _eTankAIDriver;
    public ItemSpawner _itemSpawner;

    public Material DeathCamo;

    public List<MeshRenderer> carbody;

    [Header("Wheel Colliders")]
    public List<WheelCollider> powerTyres;
    public List<WheelCollider> steerTyres;
    



    void Awake()
    {
        //Colliders
        powerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RL_Wheel").gameObject.GetComponent<WheelCollider>();
        powerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RR_Wheel").gameObject.GetComponent<WheelCollider>();

        steerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FL_Wheel").gameObject.GetComponent<WheelCollider>();
        steerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FR_Wheel").gameObject.GetComponent<WheelCollider>();
    }
    
    void Start()
    {

        //healthSlider = transform.Find("Enemy Canvas").transform.Find("HealthBar").gameObject;

        _enemyTankMovement = this.GetComponent<EnemyTankMovement>();
        _eTankAIDriver = this.GetComponent<ETankAIDriver>();
        _itemSpawner = this.GetComponent<ItemSpawner>();


        



        cameraPivotPoint = GameObject.Find("CameraPivotPoint").GetComponent<Transform>();
        
        currentHealth = maxHealth;

        healthSlider.GetComponent<Slider>().maxValue = maxHealth;
    }

    void Update()
    {
        if(healthSlider)
        {
            healthSlider.transform.rotation = cameraPivotPoint.rotation;
            UpdateSliderValue(currentHealth, healthSlider.GetComponent<Slider>());
        }
        
    }

    public void decreaseHealth(float health)
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
        this.gameObject.tag = "Dead";
        if(this.GetComponent<Animator>())
            this.GetComponent<Animator>().SetBool("IsDead", true);
        _eTankAIDriver.enabled= false;
        _enemyTankMovement.enabled=false;

        for(int i = 0; i< powerTyres.Count; i++)
        {
            powerTyres[i].gameObject.SetActive(false);
            steerTyres[i].gameObject.SetActive(false);
            
            
        }


        for(int i = 0; i<carbody.Count; i++)
        {
            carbody[i].material = DeathCamo;
        }
        healthSlider.SetActive(false);

        

        
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider)
        {
                decreaseHealth(Random.Range(0.1f,0.2f));
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Shell")
        {
            decreaseHealth(Random.Range(5,10));
        }
    }

  
    void UpdateSliderValue(float value, Slider sliderType)
    {
        
        sliderType.value = value;
    }

    
}
