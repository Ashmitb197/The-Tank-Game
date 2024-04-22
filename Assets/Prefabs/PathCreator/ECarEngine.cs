using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECarEngine : MonoBehaviour
{

    public Transform path;
    private List<Transform> nodes;
    private int currentNode = 0;

    public float maxSteerAngle = 30;


    [Header("Wheel Colliders")]
    public List<WheelCollider> powerTyres;
    public List<WheelCollider> steerTyres;
    // Start is called before the first frame update

    void Awake()
    {
        powerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RL_Wheel").gameObject.GetComponent<WheelCollider>();
        powerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("RR_Wheel").gameObject.GetComponent<WheelCollider>();

        steerTyres[0] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FL_Wheel").gameObject.GetComponent<WheelCollider>();
        steerTyres[1] = transform.Find("Wheels").transform.Find("Colliders").transform.Find("FR_Wheel").gameObject.GetComponent<WheelCollider>();        
    }
    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();

        for(int i = 0; i<pathTransforms.Length; i++)
        {
            if(pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplySteer();
    }

    void ApplySteer()
    {
        Vector3 relativeVector =  transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x/relativeVector.magnitude)*maxSteerAngle;

        for(int i =0; i<steerTyres.Count; i++)
        {
            steerTyres[i].steerAngle = newSteer;
        }

    }
}
