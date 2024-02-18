using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    public AudioSource runningSound;
    public float runningMaxVolume;
    public float runningMaxPitch;

    public AudioSource idleSound;
    public float idleMaxVolume;

    private float SpeedRatio;

    private TankController tankMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        tankMovementScript = GetComponent<TankController>();
        runningSound = transform.Find("RunningSound").gameObject.GetComponent<AudioSource>();
        idleSound = transform.Find("IdleSound").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tankMovementScript != null)
        {
            SpeedRatio = tankMovementScript.GetSpeedRatio();
        }
        if(tankMovementScript.currentSpeed == 0)
            idleSound.volume = Mathf.Lerp(0.1f, idleMaxVolume, SpeedRatio);

        else{
            runningSound.volume = Mathf.Lerp(0.3f, runningMaxVolume, SpeedRatio);
            runningSound.pitch = Mathf.Lerp(runningSound.pitch,SpeedRatio, Time.deltaTime);
        }
        
    }
}
