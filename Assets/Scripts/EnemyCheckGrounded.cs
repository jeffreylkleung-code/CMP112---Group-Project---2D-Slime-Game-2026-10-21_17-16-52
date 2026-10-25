using UnityEngine;

public class EnemyDetectGround : MonoBehaviour
{

    //Checks if it is touching the ground and tells if it is grounded


    private EnemyMovementController parent;

    bool isGrounded;

    


    void Start()
    {
        parent = GetComponentInParent<EnemyMovementController>();

        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        parent.CheckGrounded(isGrounded);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        parent.CheckGrounded(isGrounded);
    }


}
