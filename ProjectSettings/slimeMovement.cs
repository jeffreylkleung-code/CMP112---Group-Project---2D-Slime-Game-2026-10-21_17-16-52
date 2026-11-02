using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class slimeMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumping;
    public float speed;
    public float tempJumping = 0;
    GameObject floor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floor = GameObject.Find("floor");
        rb = GetComponent<Rigidbody2D>();
    }

    //https://discussions.unity.com/t/moving-2d-objects-with-wasd-arrow-keys-at-a-constand-speed-c/633247/2
    public Vector3 CalculateD()
    {
        Vector3 horizontalDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
        {
            horizontalDirection.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalDirection.x -= 1.0f;
        }
        return horizontalDirection.normalized;
    }

    void OnTriggerEnter(Collider other)
    {
        speed = Vector3.Magnitude(rb.linearVelocity);
        if (speed > 1.0f)
        {
            tempJumping = tempJumping / 2;
            rb.AddForce(Vector2.up * tempJumping, ForceMode2D.Impulse);
        } else
        {
            tempJumping = 0;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = CalculateD();
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempJumping = jumping;
            //gamedevbeginner how to jump in unity
            rb.AddForce(Vector2.up * jumping, ForceMode2D.Impulse);
        }

        
    }
}
