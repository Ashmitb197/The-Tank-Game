using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETankAIDriver : MonoBehaviour
{
    public Transform targetPositionTransform;
    public EnemyTankMovement enemyTankMovement;
    public Vector3 targetPosition;

    [Header("Vechicle Working")]

    public float forwardAmount = 0f;
    public float turnAmount = 0f;
    public float maxReverseDistance = 5f;
    public float maxReachedDistance = 10f;

    [Header ("Ray Hits")]
    public bool forwardHit;
    public bool backHit;
    public bool rightHit;
    public bool leftHit;
    public bool left45Hit;
    public bool right45Hit;
    
    public float rayDistance;
    
    public LayerMask collisionPreset;

    // Start is called before the first frame update
    void Start()
    {
        targetPositionTransform = GameObject.Find("Player_Tank").GetComponent<Transform>();
        enemyTankMovement = this.GetComponent<EnemyTankMovement>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AIWorking();
        obstacleDetection();
    }

    void AIWorking()
    {
        setTargetPosition(targetPositionTransform.position);


        float distToTarget = Vector3.Distance(this.transform.position, targetPosition);

        Vector3 dirToTarget = (targetPosition - this.transform.position).normalized;
        float dotProduct  = Vector3.Dot(transform.forward, dirToTarget);
        if(!forwardHit && !left45Hit && !right45Hit)
        {
            if(distToTarget >  maxReachedDistance)
            {

                if(dotProduct>0)
                {
                    forwardAmount = 1f;
                }
                else
                {
                    if(distToTarget < maxReverseDistance)
                        forwardAmount = -1f;
                    
                    else
                        forwardAmount = 1f;
                }

                float angletoDir = Vector3.SignedAngle(transform.forward, dirToTarget, Vector3.up);

                if(angletoDir > 0f)
                {
                    turnAmount = Mathf.Lerp(turnAmount, 1 ,0.05f);
                }
                else
                {
                    turnAmount = Mathf.Lerp(turnAmount, -1 ,0.05f);;
                }

                
            
            }
            else
            {
                forwardAmount = 0f;
                turnAmount = 0f;
            }
        
        }
        
        // if(forwardHit && right45Hit && left45Hit)
        // {
        //     forwardAmount = -5;
        //     turnAmount = 0;
        // }
        // if(forwardHit || (!right45Hit && left45Hit))
        // {
        //     forwardAmount = 1;
        //     turnAmount = 1;
        // }
        // if(forwardHit || (right45Hit && !left45Hit))
        // {
        //     forwardAmount = 1;
        //     turnAmount = -1;
        // }


        if(right45Hit)
        {
            turnAmount = -1;
        }
        if(left45Hit)
        {
            turnAmount = 1;
        }
        if(forwardHit)
        {
            forwardAmount = -5;
            turnAmount = 0;
        }

        enemyTankMovement.setInputs(forwardAmount, turnAmount);
    }

    void obstacleDetection()
    {
        RaycastHit hit;

        forwardHit = Physics.Raycast(transform.position, transform.forward, out hit, rayDistance-0.5f, collisionPreset);
        backHit = Physics.Raycast(transform.position, transform.forward, out hit, -rayDistance, collisionPreset);

        left45Hit = Physics.Raycast(transform.position, -(transform.right-transform.forward).normalized, rayDistance, collisionPreset);
        right45Hit = Physics.Raycast(transform.position, (transform.right+transform.forward).normalized, rayDistance, collisionPreset);

        rightHit = Physics.Raycast(transform.position, transform.right, out hit, rayDistance, collisionPreset);
        leftHit = Physics.Raycast(transform.position, -transform.right, out hit, rayDistance, collisionPreset);
       

        Debug.DrawRay(transform.position, transform.forward*(rayDistance-0.5f), forwardHit?Color.red:Color.green);
        Debug.DrawRay(transform.position, transform.forward*-rayDistance, backHit?Color.red:Color.green);
        Debug.DrawRay(transform.position, transform.right*rayDistance, rightHit?Color.red:Color.green);
        Debug.DrawRay(transform.position, transform.right*-rayDistance, leftHit?Color.red:Color.green);
        Debug.DrawRay(transform.position, (transform.right-transform.forward).normalized* -rayDistance, left45Hit?Color.red:Color.green);//left
        Debug.DrawRay(transform.position, (transform.right+transform.forward).normalized* rayDistance, right45Hit?Color.red:Color.green);
    }

    public void setTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    IEnumerator reverse()
    {
        yield return new WaitForSeconds(0.5f);

        turnAmount = 0;
        forwardAmount = -1;

        yield return new WaitForSeconds(2.0f);
    }
}
