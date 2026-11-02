using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{

    //Enemy Health
    public float Health;

    public Rigidbody2D rb;

    //Patrol settings
    public bool isPatrolling;
    public Transform[] PatrolPoints;
    private int patrolDestination; // only public for debugging, make private

    // Idling
    private bool isIdle; 
    public float idleTime; 
    private float idleTime_Count; // only public for debugging, make private

    //Movement
    public float moveSpeed_Defualt;
    public float moveSpeed_Aggro;
    public float JumpPower;
    public float JumpCooldown;
    public float JumpCooldown_Aggro;
    private float JumpCooldown_Count; // only public for debugging, make private


    //Detection
    private bool wallDetected;
    public bool IsGrounded;
    private RaycastHit2D DetectedTarget;
    public LayerMask DetectionLayer;



    //Player Aggro
    public bool isAggressive;
    public string targetTag;
    private List<Collider2D> TargetsInRange;
    private bool targetFound; // only public for debugging, make private
    private Transform targetTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        if (isIdle)
        {
            if (idleTime_Count < idleTime)
            {
                idleTime_Count += (1 * Time.deltaTime);
            }
            else
            {
                isIdle = false;
                idleTime_Count = 0;
            }
        }

        

        //If aggroed on enemy, chase target
        if (targetFound)
        {
            //Check if targets within range
            if (isAggressive)
            {
                FindTarget();
            }
            
            
            Debug.DrawRay(transform.position, (targetTransform.position - transform.position).normalized);// DEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUGDEBUG
            
        }
        // if enemy not aggrroed, patrol
        else
        {
            if (!isIdle && isPatrolling)
            {
                EnemyPatrol();
            }
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
        if (transform.parent)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);

    }


    /// PatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrolling
    /// Patrolling mechanics
    /// PatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrollingPatrolling

    // Patrolling mechanic 
    private void EnemyPatrol()
    {

        if (wallDetected)
        {

            //if jump cooldown is ready
            if (JumpCooldown_Count >= JumpCooldown)
            {
                jump();
            }

        }
        else
        {
            //Move towards patrol point
            transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[patrolDestination].position, moveSpeed_Defualt * Time.deltaTime);

        }


        if (Vector2.Distance(transform.position, PatrolPoints[patrolDestination].position) < .2f)
        {
            SwitchPatrolRoute();

            //Checks if patrol point is on the right
            faceToTarget(transform.position, PatrolPoints[patrolDestination].position);
        }
    }

    // Switches patrol destinations in case there is more than 2 point
    private void SwitchPatrolRoute()
    {
        isIdle = true;
        if (patrolDestination + 1 > PatrolPoints.Length - 1)
        {
            patrolDestination = 0;
            print(patrolDestination);
        }
        else
        {
            patrolDestination += 1;
            print(patrolDestination);
        }

    }
    //Checks if target location is on the right, and flip the direction accordingly
    private void faceToTarget(Vector2 OriginPosition, Vector2 TargetPosition)
    {
        if (isTargetPositionRight(OriginPosition, TargetPosition)){

            FlipToRight(true);

        }
        else
        {
            FlipToRight(false);
        }


    }

    // Checks if target location is on the right
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

        if (isObjectInFront == true && detectedObject.gameObject.CompareTag("Map"))
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


    // Finding targets

    private void FindTarget()
    {

        //Check if target is on left or right
        faceToTarget(transform.position, targetTransform.position);

        if (wallDetected)
        {
            if (JumpCooldown_Count >= JumpCooldown_Aggro)
            {
                jump();

            }
        }
        else
        {
            GoToTarget();
        }
       
    }

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
        }
        else
        {
            targetFound = false;
        }
    }
    
    private void GoToTarget()
    {
        
        //Go to left to target
        if (Vector2.Distance(transform.position, targetTransform.position) > 1f)
        {
            if (transform.position.x > targetTransform.position.x)
            {
                transform.position += Vector3.left * (moveSpeed_Aggro * Time.deltaTime);
            }
            else
            {
                transform.position += Vector3.right * (moveSpeed_Aggro * Time.deltaTime);
            }
        }
    }

    
    


}
