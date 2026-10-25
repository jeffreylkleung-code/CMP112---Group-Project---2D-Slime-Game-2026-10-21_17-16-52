using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyMovementController : MonoBehaviour
{

    public Rigidbody2D rb;
    public BoxCollider2D JumpColliderBox;

    //Patrol settings
    public Transform Target;
    public Transform[] PatrolPoints;
    public int patrolDestination;

    //Movement
    public float moveSpeed_Defualt;
    public float moveSpeed_Agrro;
    public float JumpPower;
    public float JumpCooldown;
    public float JumpCooldown_Count;
    private bool wallDetected;
    public bool IsGrounded;



    //Player Aggro
    private bool TargetFound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetFound = false;
        IsGrounded = false;
        wallDetected = false;

        JumpCooldown_Count = 0;


    }

    void Update()
    {

        //Cooldowns
        JumpCooldown_Count += (1 * Time.deltaTime);


        //If aggroed on enemy, attack
        if (TargetFound)
        {

        }
        // if enemy not aggrroed, patrol
        else
        {
            EnemyPatrol();
        }



    }

    // Patrolling mechanic 
    private void EnemyPatrol()
    {

        if (wallDetected)
        {

            //if jump cooldown is ready
            if (JumpCooldown_Count >= JumpCooldown)
            {

                jump();
                JumpCooldown_Count = 0;

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
            if (transform.position.x <= PatrolPoints[patrolDestination].position.x)
            {
                // Flips the enemy direction (sprite and colliders etc) to right
                transform.localScale = new Vector3(1, 1, 1);

            }
            //on the left
            else
            {
                // Flips the enemy direction (sprite and colliders etc) to left
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }

    }

    // Switches patrol destinations in case there is more than 2 point
    private void SwitchPatrolRoute()
    {

        if (patrolDestination+1 > PatrolPoints.Length-1)
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

    // Function for EnemyCheckGrounded to call; Detects what object is in front and act accordingly
    public void DetectJump(bool isObjectInFront ,Collider2D detectedObject)
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

        //print(IsGrounded);

    }
     

    private void jump()
    {
        rb.AddForce(Vector2.up * JumpPower);
    }






}
