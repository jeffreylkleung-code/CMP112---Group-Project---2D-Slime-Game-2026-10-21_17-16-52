using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class slimeMovement : MonoBehaviour
{
    public bool isJumping;
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

    //discussions.unity.com/t/moving-2d-objects-with-wasd-arrow-keys-at-a-constand-speed-c/633247/2
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


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = CalculateD();
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        bool isJumping = GameObject.Find("JumpCheck").GetComponent<jumpCheckScript>().isJumping;


        if (isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            tempJumping = jumping;
            //gamedevbeginner how to jump in unity
            rb.AddForce(Vector2.up * jumping, ForceMode2D.Impulse);
        }

    }
}
