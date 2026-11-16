using System.Collections;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class slimeMovement : MonoBehaviour
{
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floor = GameObject.Find("floor");
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    //discussions.unity.com/t/moving-2d-objects-with-wasd-arrow-keys-at-a-constand-speed-c/633247/2

    // Update is called once per frame
    Vector2 CalculateD()
    {
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
            //ttps://stackoverflow.com/questions/43714781/proper-way-to-move-rigidbody-gameobject
            Vector2 moveVector = CalculateD();
            rb.AddForce(moveVector * moveSpeed);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
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
        if (collision.gameObject.tag == "Collision Object")
        {
            boxCheck = false;
        }
    }
    void stopper()
    {
        StopCoroutine(nameof(TimerDelay));
    }
    IEnumerator TimerDelay()
    {

        yield return new WaitForSeconds(3);
        gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        stopper();

    }


    private void Update()
    {
        publicScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
        sizeController = new Vector3 (1.0f, 1.0f, 1.0f);
        
   
        if (Input.GetKeyDown(KeyCode.Space) && boxCheck)
        {
            tempJumping = jumping;
            //gamedevbeginner how to jump in unity
            rb.AddForce(Vector2.up * jumping, ForceMode2D.Impulse);
        }
            else if (Input.GetKeyDown(KeyCode.Space) && isJumping)
        {
            tempJumping = jumping;
            //gamedevbeginner how to jump in unity
            rb.AddForce(Vector2.up * jumping, ForceMode2D.Impulse);
        }

        //if(publicScale.x <  sizeController.x)
        //{
        //    StartCoroutine(TimerDelay());
        //        
        //}
    }
}
