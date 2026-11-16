using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

//This player controller uses the old input manager

public class slimeMovement : MonoBehaviour
{
    //Variable initialisations, some are used for testing.
    public Vector3 publicScale;
    public Vector3 sizeController;
    public bool isJumping;
    public bool gameOver;
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumping;
    public float tempJumping = 0;
    public bool boxCheck = false;
    GameObject floor;
    void Start()
    {
        //At the start, set important variables.
        floor = GameObject.Find("floor");
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }


    Vector2 CalculateD()
    {    //The below code was adapted from the discussion board linked below.
         //https://discussions.unity.com/t/moving-2d-objects-with-wasd-arrow-keys-at-a-constand-speed-c/633247/4
        Vector2 horizontalDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            horizontalDirection.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalDirection.x -= 1.0f;
        }
        return horizontalDirection;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            //The decisions around how to move using rigidbody were taken thanks to the code and discussions found in the below link.
            //https://stackoverflow.com/questions/43714781/proper-way-to-move-rigidbody-gameobject
            Vector2 moveVector = CalculateD();
            rb.AddForce(moveVector * moveSpeed);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //If the object is a collision object, e.g. a box, allow the player to jump.
        if (collision.gameObject.tag == "Collision Object")
        {
            boxCheck = true;
        }
        else
        {
            boxCheck = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Removes ability to jump usin collisio objects when not on a collision object
        if (collision.gameObject.tag == "Collision Object")
        {
            boxCheck = false;
        }
    }
    private void Update()
    {
        publicScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
        sizeController = new Vector3 (1.0f, 1.0f, 1.0f);
        
        //Allows jumping if one of two bools are true, being boxCheck and is jumping (which is set from the jumpCheck program).
        if (Input.GetKeyDown(KeyCode.Space) && boxCheck)
        {
            tempJumping = jumping;
            //Code learned from GameDevBeginner, how to jump in unity, link shown below:
            //https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/
            rb.AddForce(Vector2.up * jumping, ForceMode2D.Impulse);
        }
            else if (Input.GetKeyDown(KeyCode.Space) && isJumping)
        {
            tempJumping = jumping;
            rb.AddForce(Vector2.up * jumping, ForceMode2D.Impulse);
        }

    }
}
