using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable, IAbsorbable
{
    // Enemy States
    enum EnemyState { STATE_NONE, STATE_IDLE, STATE_PATROLLING, STATE_AGGROTARGET, STATE_FEARTARGET } // None is unused
    EnemyState CurrentState;
    public bool stateComplete;

    // State Behaviours
    public bool isPatrolling; // if true, patrol between points
    public bool isAggressive; // If true, be aggresive to target; go towards target
    public bool fearTarget; // if true, be scared of target; go away target

    // Player Absorbtion
    public float ObjectSize;
    public float ReturnSizeValue;
    private bool ObjectAbsorbed = false;

    //Enemy Health
    public float Health;

    public Rigidbody2D rb;
    public Transform ParentObject;

    //Patrol settings
    public Transform[] PatrolPoints;
    private int patrolDestination; // only public for debugging, make private

    // Idling
    private bool isIdle; 
    public float idleTime; 
    public float idleTime_Count; // only public for debugging, make private

    //Movement
    public float moveSpeed_Defualt;
    public float moveSpeed_Aggro;
    public float JumpPower;
    public float JumpCooldown;
    public float JumpCooldown_Aggro;
    private float JumpCooldown_Count; // only public for debugging, make private


    //Detection
    private bool wallDetected;
    public string wallTag;
    public bool IsGrounded; // only public for debugging, make private
    private RaycastHit2D DetectedTarget;
    public LayerMask DetectionLayer;



    //Player Aggro
    public string targetTag;
    private List<Collider2D> TargetsInRange;
    private bool targetFound; // only public for debugging, make private
    private Transform targetTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        CurrentState = EnemyState.STATE_IDLE;
        stateComplete = true;


        isIdle = false;
        IsGrounded = false;
        wallDetected = false;
        JumpCooldown_Count = 0;

        targetFound = false;
        
        if (isPatrolling)
        {
            faceToTarget(transform.position, PatrolPoints[patrolDestination].position);
        }

    }
    
    void Update()
    {

        //Cooldowns
        if (JumpCooldown_Count < JumpCooldown)
        {
            JumpCooldown_Count += (1 * Time.deltaTime);
        }


        // State
        if (stateComplete)
        {
            ChangeStates();
            Debug.Log(CurrentState); //DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG
        }
        UpdateState();

    }

    /// StatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStates
    /// Enemy States
    /// StatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStatesStates

    void ChangeStates()
    {
        
        // States
        if (targetFound)
        {
            if (isAggressive)
            {
                CurrentState = EnemyState.STATE_AGGROTARGET;
                
            }
            else if (fearTarget)
            {
                CurrentState = EnemyState.STATE_FEARTARGET;
            }
            else
            {
                CurrentState = EnemyState.STATE_IDLE;
            }
        }
        else if (isPatrolling) // if idle cooldown is ready
        {
            CurrentState = EnemyState.STATE_PATROLLING;
            faceToTarget(transform.position, PatrolPoints[patrolDestination].position);
        }
        else
        {
            CurrentState = EnemyState.STATE_IDLE;
        }

        stateComplete = false;

    }

    void UpdateState()
    {

        switch (CurrentState)
        {
            case EnemyState.STATE_IDLE:
                {
                    StateIdle();
                    break;
                }
            case EnemyState.STATE_PATROLLING:
                {
                    StatePatrolling();
                    break;
                }
            case EnemyState.STATE_AGGROTARGET:
                {
                    StateAggroTarget();
                    break;
                }
            case EnemyState.STATE_FEARTARGET:
                {
                    StateFearTarget();
                    break;
                }
            default:
                {
                    stateComplete = true;
                    break;
                }
        }
    }

    
    void StateIdle()
    {
        // Idle Cooldown

        if (idleTime_Count >= idleTime || targetFound)
        {
            idleTime_Count = 0;
            stateComplete = true;
        }
        else
        {
            idleTime_Count += (1 * Time.deltaTime);
        }

    }

    void StatePatrolling()
    {
        //Variables 
        if (targetFound)
        {
            ChangeStates();
        }

        //Patrol

        if (wallDetected)
        {
            if (JumpCooldown_Count >= JumpCooldown)
            {
                jump();
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[patrolDestination].position, moveSpeed_Defualt * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, PatrolPoints[patrolDestination].position) < .2f)
        {
            SwitchPatrolRoute();

            //Checks if patrol point is on the right
            faceToTarget(transform.position, PatrolPoints[patrolDestination].position);
            CurrentState = EnemyState.STATE_IDLE; // Make enemy stand for some time to cool down
        }

    }

    void StateAggroTarget()
    {
        faceToTarget(transform.position, targetTransform.position);

        GoToTarget();

        if (wallDetected && JumpCooldown_Count >= JumpCooldown_Aggro)
        {
            jump();
            JumpCooldown_Count = 0;
        }

        if (Vector2.Distance(transform.position, targetTransform.position) < 1f  || !targetFound || !isAggressive)
        {
            stateComplete = true;
        }

    }

    void StateFearTarget()
    {
        faceToTarget(targetTransform.position, transform.position);

        GoAwayTarget();

        if (wallDetected && JumpCooldown_Count >= JumpCooldown_Aggro)
        {
            jump();
            JumpCooldown_Count = 0;
        }

        if (!targetFound || !fearTarget)
        {
            CurrentState = EnemyState.STATE_IDLE;
        }

    }
    


    /// HealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealth
    /// Enemy Health
    /// HealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealthHealth

    public void Damage(float DamageTaken)
    {
        Health -= DamageTaken;
        CheckHealth();
    }

    // Checks if the enemy has 0 hp
    private void CheckHealth()
    {
        if (Health <= 0)
        {
            print("Enemy is dead");
            Kill();
        }

    }
    public void Kill()
    {
        // Checks if object has parent to destroy everything about object (patrol points etc.)
        if (ParentObject)
        {
            Destroy(ParentObject);
        }
        Destroy(gameObject);

    }


    /// PatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrolling
    /// Patrolling mechanics
    /// PatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrolling


    // Switches patrol destinations in case there is more than 2 point
    private void SwitchPatrolRoute()
    {
        if (patrolDestination + 1 > PatrolPoints.Length - 1)
        {
            patrolDestination = 0;
            //print(patrolDestination); // DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG
        }
        else
        {
            patrolDestination += 1;
            //print(patrolDestination); // DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG
        }
    }

    // Flips the enemy direction (sprite and colliders etc) to right, set to false if left
    private void FlipToRight(bool isRight)
    {

        if (isRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Function for EnemyCheckGrounded to call; Detects what object is in front and act accordingly
    public void DetectJump(bool isObjectInFront, Collider2D detectedObject)
    {

        if (isObjectInFront == true && detectedObject.gameObject.CompareTag(wallTag))
        {
            wallDetected = true;
        }
        else
        {
            wallDetected = false;
        }

    }

    public void CheckGrounded(bool isGrounded)
    {
        IsGrounded = isGrounded;
    }


    private void jump()
    {
        rb.AddForce(Vector2.up * JumpPower);
        JumpCooldown_Count = 0;
    }

    /// TargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargetting
    /// Targetting mechanics
    /// TargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargettingTargetting


    // Checks if target location is on the right, and flip the direction accordingly
    // can swap positions to face away from target
    private void faceToTarget(Vector2 OriginPosition, Vector2 TargetPosition)
    {
        if (isTargetPositionRight(OriginPosition, TargetPosition))
        {
            FlipToRight(true);
        }
        else
        {
            FlipToRight(false);
        }
    }

    // Checks if target location is on the right
    // Takes in self and target position; Returns true or left depending if they are right or left
    private bool isTargetPositionRight(Vector2 OriginPosition, Vector2 TargetPosition)
    {
        // On the right
        if (OriginPosition.x <= TargetPosition.x)
        {
            return true;
        }
        // On the left
        else
        {
            return false;
        }
    }

    // Called by Detection range script
    // Updates the current list of targets in range
    public void UpdateCurrentTargetsInRange(List<Collider2D> targets)
    {

        TargetsInRange = targets;

        if (TargetsInRange.Count > 0)
        {
            // Check if the target is behind a wall
            for (int i = 0; i < TargetsInRange.Count; i++)
            {
                DetectedTarget = Physics2D.Raycast(transform.position, (TargetsInRange[i].transform.position - transform.position).normalized, Mathf.Infinity, DetectionLayer);

                if (DetectedTarget.transform.gameObject.CompareTag(targetTag))
                {
                    targetTransform = TargetsInRange[i].transform; // only targets the first target that is in range
                    targetFound = true;
                }
                else
                {
                    targetFound = false;
                }        
            }   

            UpdateState(); // Update the state when target found
            
            //Check if the target is larger than self; fear target if so



        }
        else
        {
            targetFound = false;
        }
    }
    
    // Go towards the target
    
    private void GoToTarget()
    {
       
        if (Vector2.Distance(transform.position, targetTransform.position) > 1f)
        {
            //Go to left to target
            if (transform.position.x > targetTransform.position.x)
            {
                transform.position += Vector3.left * (moveSpeed_Aggro * Time.deltaTime); 
            }
            // go right to target
            else
            {
                transform.position += Vector3.right * (moveSpeed_Aggro * Time.deltaTime);
            }
        }
    }

    private void GoAwayTarget()
    {
        if (Vector2.Distance(transform.position, targetTransform.position) > 1f)
        {
            //Go to left to target
            if (transform.position.x > targetTransform.position.x)
            {
                transform.position += Vector3.right * (moveSpeed_Aggro * Time.deltaTime);
            }
            // go right to target
            else
            {
                transform.position += Vector3.left * (moveSpeed_Aggro * Time.deltaTime);
            }
        }
    }

    public float Absorb(float PlayerSizeValue)
    {
        // If the player is larger than object, absorb, increase player size, and delete object
        if (PlayerSizeValue >= ObjectSize && !ObjectAbsorbed)
        {

            ObjectAbsorbed = true;
            Kill();
            return ReturnSizeValue;

        }
        // else, return 0, which mean the player didnt do anything to it
        else
        {
            return 0;
        }
    }
}
