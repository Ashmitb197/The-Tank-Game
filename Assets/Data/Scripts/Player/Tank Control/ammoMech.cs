using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GunType
{
    Launcher,
    RapidFire
};


public class ammoMech : MonoBehaviour
{
    public GameObject turretRefrence;
    public GameObject muzzlePointRefrence;
    public GameObject LauncherPointRefrence;
    public GameObject bullet;

    [Header("Launcher Settings")]
    public GameObject secondaryWeapon;



    [Header("Rapid Fire Ammo")]

    public int maxMAG;
    public int currentMAG;
    public int maxAmmo = 40;
    public int currentAmmo;

    public ParticleSystem muzzleFlash;

    [Header("Launcher Ammo")]
    public int maxMissile;
    public int currentMissileCount;

    public bool IsSecondaryWeaponInUse;

    //public turretRotation turretRotRef;

    [Header("Fire Rate Time(S)")]
    public float RapidFireReloadTime;
    
    public float LauncherReloadTime;
    bool isReloadOver;
    float lastTime;

    float gunSelection;
    public GunType guns;

    public Animator anim;

    [Header("UI Management")]
    public Text RapidFireText;
    public Text LauncherText;

    [Header("Audio Sources")]
    public AudioSource reloadSound;
    public AudioSource shootSound;

    [Header("Audio Clips")]
    public AudioClip Launcher_Clip;
    public AudioClip Reload_Clip;
    public AudioClip RapidFire_Clip;





    [Header("Debug")]
    [Range(0,1)] public float debugTimeScale;


    void Awake()
    {

        
        secondaryWeapon = this.transform.Find("PlayerTankLauncher").gameObject;
        //secondaryWeapon.GetComponent<SecondaryWeapon>().MaxLauncherAmmo
        
        
    }
    // Start is called before the first frame update
    void Start()
    {

        currentMissileCount = maxMissile;

        anim = this.GetComponent<Animator>();
        RapidFireText = GameObject.Find("AmmoSystem").transform.Find("RapidFire").transform.Find("AmmoText").GetComponent<Text>();
        LauncherText = GameObject.Find("AmmoSystem").transform.Find("Launcher").transform.Find("AmmoText").GetComponent<Text>();
        

        turretRefrence = transform.Find("Turret").gameObject;
        muzzlePointRefrence = turretRefrence.transform.Find("Muzzle").transform.Find("MuzzlePoint").gameObject;

        muzzleFlash = muzzlePointRefrence.transform.Find("MuzzleFlash01").GetComponent<ParticleSystem>();

        //RAPIDFIREPOINT TO BE RANMED TO LAUNCHER FIRE
        LauncherPointRefrence = turretRefrence.transform.Find("RapidFirePoint").gameObject;

       

        isReloadOver = true;

        reloadSound = this.transform.Find("Reload").GetComponent<AudioSource>();
        shootSound = this.transform.Find("Shoot").GetComponent<AudioSource>();

        guns = GunType.RapidFire;


        currentMissileCount = maxMissile;
        currentMAG = maxMAG;
        currentAmmo = maxAmmo;



    }

    void DebugActions()
    {
        Time.timeScale = debugTimeScale;

        if(Input.GetKeyDown("r"))
        {
            this.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DebugActions();
        //gunSelectionInput();

        switch(guns)
        {
            case GunType.Launcher:
                if(secondaryWeapon && secondaryWeapon.activeSelf)
                {

                    shootBulletProjectileForLauncher();
                    
                    
                }
                else
                {
                    guns = GunType.RapidFire;
                }
                break;
            
            case GunType.RapidFire:
                IsSecondaryWeaponInUse = false;
                shootBulletProjectileForRapidFire();
                anim.SetBool("SecondaryWeaponActivated", false);

                if(secondaryWeapon)
                {
                    secondaryWeapon.GetComponent<Animator>().SetBool("IsLauncherActivated", false); 
                }
                break;
        }


            

        
    }

    void gunSelectionInput()
    {
        gunSelection = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(gunSelection);

        switch(gunSelection)
        {
            case 0.1f:
                    if(guns == GunType.Launcher)
                        guns = GunType.RapidFire;
                    else
                        guns = GunType.Launcher;
                    break;
            
            case -0.1f:
                     if(guns == GunType.Launcher)
                        guns = GunType.RapidFire;
                    else
                        guns = GunType.Launcher;
                    break;
        }

        
    }

    public void shootBulletProjectileForLauncher()
    {
        UpdateAudioClipToAudioSource(Reload_Clip, reloadSound);
        UpdateAudioClipToAudioSource(Launcher_Clip, shootSound);

        RapidFireText.gameObject.transform.parent.gameObject.SetActive(false);
        LauncherText.gameObject.transform.parent.gameObject.SetActive(true);
        UpdateAmmoText(LauncherText, currentMissileCount.ToString());

        secondaryWeapon.GetComponent<Animator>().SetBool("IsLauncherActivated", true);

        if(Input.GetButton("Fire1") && (currentMissileCount > 0))
        {
            // Instantiate(bullet, LauncherPointRefrence.transform.position, LauncherPointRefrence.transform.rotation);

            // StartCoroutine(playShootAndReloadSoundRapid());
            //shootSound.Play();
            
            currentMissileCount -= 6;

            
            anim.SetBool("SecondaryWeaponActivated", true);
            IsSecondaryWeaponInUse = true;

            if(currentMissileCount < 0)
            {
                currentMissileCount = 0;
            }
        }
    }

    public void shootBulletProjectileForRapidFire()
    {
        UpdateAudioClipToAudioSource(Reload_Clip, reloadSound);
        UpdateAudioClipToAudioSource(RapidFire_Clip, shootSound);
        RapidFireText.gameObject.transform.parent.gameObject.SetActive(true);
        LauncherText.gameObject.transform.parent.gameObject.SetActive(false);
        UpdateAmmoText(RapidFireText,currentMAG+" | "+currentAmmo.ToString());
        
        if(Time.time > lastTime + RapidFireReloadTime)
        { 
            if(Input.GetButtonDown("Fire1") && (currentMAG >= 0) && isReloadOver)
            {
                if(currentAmmo > 0 )
                {
                    currentAmmo -= 1;
                    
                    StartCoroutine(muzzleFlashPlay());
                    Instantiate(bullet, muzzlePointRefrence.transform.position, turretRefrence.transform.rotation);

                    // if(!shootSound.isPlaying)
                    // {
                        
                    // }
                    //this.GetComponent<Rigidbody>().AddForce(muzzlePointRefrence.transform.forward*-50000);
                    
                    lastTime = Time.time;
                }
                else
                {
                    
                    
                    StartCoroutine(playShootAndReloadSoundRapid());
                     
                }
                  
            }
        }
    }

    IEnumerator playShootAndReloadSound()
    {
        shootSound.Play();
        yield return new WaitForSeconds(1.750f);
        reloadSound.Play();
    }

    IEnumerator playShootAndReloadSoundRapid()
    {
        isReloadOver = false;
        reloadSound.Play();
        yield return new WaitForSeconds(1.750f);
        currentAmmo = maxAmmo;
        currentMAG--;
        
        isReloadOver = true;
    }


    void UpdateAmmoText(Text textType, string currentAmmo)
    {
        textType.text = currentAmmo;
    }

    void UpdateAudioClipToAudioSource(AudioClip audio, AudioSource audioSource)
    {
        audioSource.clip = audio;
    }


    IEnumerator muzzleFlashPlay()
    {
        
        muzzleFlash.Play();
        yield return new WaitForSeconds(0.3f);
        muzzleFlash.Stop();
    }
    

    


    

    
}
