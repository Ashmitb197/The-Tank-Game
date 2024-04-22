using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{

    public float maxHealth = 100;
    private float currentHealth;

    public GameObject HealthUISystem;
    
    public Slider healthSlider;
    public Text healthTextMeter;

    [Header("Armour Elements")]
    public GameObject armourSlider;
    public float maxArmourHealth = 50;
    public float currentArmourHealth;
    public float armourActivationTime;
    public bool isArmourActive;

    [Header("Wheel Colliders")]
    public List<WheelCollider> powerTyres;
    public List<WheelCollider> steerTyres;

    [Header("Vehicle Scripts")]
    public TankController _tankController;
    public FuelSystem _fuelSystem;
    public turretRotation _turretRotation;
    public ammoMech _ammoMech;

    public Transform deathCam;

    public GameObject _HUD;
    public followCamera _followCamera;
    public Material deathCamo;
    public List<MeshRenderer> meshRenderer;
    

    void Awake()
    {

        powerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RL_Wheel").gameObject.GetComponent<WheelCollider>();
        powerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RR_Wheel").gameObject.GetComponent<WheelCollider>();

        steerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FL_Wheel").gameObject.GetComponent<WheelCollider>();
        steerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FR_Wheel").gameObject.GetComponent<WheelCollider>();

        HealthUISystem = GameObject.Find("HealthUISystem");
        armourSlider = HealthUISystem.transform.Find("ArmourBar").gameObject;
        healthSlider = HealthUISystem.transform.Find("HealthBar").GetComponent<Slider>();
        healthTextMeter = HealthUISystem.transform.Find("HealthBar").transform.Find("HealthMeter").GetComponent<Text>();

        _ammoMech = this.GetComponent<ammoMech>();
        _fuelSystem = this.GetComponent<FuelSystem>();
        _tankController = this.GetComponent<TankController>();
        _turretRotation = this.GetComponent<turretRotation>();
        _HUD = GameObject.Find("HUD");

        _followCamera = GameObject.Find("CameraPivotPoint").GetComponent<followCamera>();
        deathCam = transform.Find("DeathCam");
    }
    void Start()
    {
        currentHealth = maxHealth;
        isArmourActive = false;
    }

    void Update()
    {
        UpdateSliderValue(currentHealth, healthSlider, maxHealth);
        UpdateSliderValue(currentArmourHealth, armourSlider.GetComponent<Slider>(), maxArmourHealth);

        
        healthTextMeter.text = currentHealth.ToString();

        armourManagment();
        
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

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {

        _HUD.SetActive(false);
        for(int i = 0; i < meshRenderer.Count; i++)
        {
            meshRenderer[i].material = deathCamo;
        }
        this.gameObject.tag = "Dead";
        _turretRotation.enabled = false;
        _tankController.enabled = false;
        _ammoMech.enabled = false;
        _fuelSystem.enabled = false;
        

        this.GetComponent<Animator>().SetBool("IsDead", true);

        _followCamera.sensitivity = 0;


    }

    void disableColliders()
    {
        for(int i = 0; i<powerTyres.Count; i++)
        {
            steerTyres[i].gameObject.SetActive(false);
            powerTyres[i].gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider)
        {
            if(isArmourActive)
                decreaseArmourHealth(Random.Range(2,5));
            
            else
                decreaseHealth(Random.Range(2,2));
        }
        
    }

    void OnTriggerEnter(Collider coll)
    {
        
        if(coll.tag == "Mine")
        {
            if(isArmourActive)
                decreaseArmourHealth(100);
            else
                decreaseHealth(50);
        }

        else if(coll.tag == "Health")
        {
            increaseHealth(Random.Range(30, 40));
        }
        else if(coll.tag == "Ammo")
        {
            _ammoMech.currentMAG += 1;
        }

        if(coll.tag == "Shell")
        {
            if(isArmourActive)
                decreaseArmourHealth(Random.Range(10,15));
            
            else
                decreaseHealth(Random.Range(2,3));
        }

        if(coll.tag == "Bullet")
        {
            if(isArmourActive)
                decreaseArmourHealth(Random.Range(3,5));
            
            else
                decreaseHealth(Random.Range(3,7));
        }

    }

    void UpdateSliderValue(float value, Slider sliderType, float maxValue)
    {
        sliderType.maxValue = maxValue;
        sliderType.value = value;
    }

    public void ActivateArmour(bool activationStatus)
    {
        isArmourActive = activationStatus;
        currentArmourHealth = maxArmourHealth;
    }

    void armourManagment()
    {
        armourSlider.SetActive(isArmourActive);
        StartCoroutine(activateArmour());
    }

    IEnumerator activateArmour()
    {
        if(Input.GetKeyDown("b"))
        {


            ActivateArmour(true);

            yield return new WaitForSeconds(armourActivationTime);

            ActivateArmour(false);
        }
    }
}
