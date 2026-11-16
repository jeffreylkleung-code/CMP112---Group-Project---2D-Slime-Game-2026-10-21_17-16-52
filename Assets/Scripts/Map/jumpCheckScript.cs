using UnityEngine;

public class jumpCheckScript : MonoBehaviour
{
    private BoxCollider2D bc;

    /* This entire piece of code was created due to the bouncy material not activating onCollisionEnter, even though it is more
     convoluted, this works better with the current Rigidbody and materials used! It controls when the player can jump, and when they cannot.*/

    private void Awake()
    {
        //Ensures that this is a trigger
        bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the player enters the JumpCheck, it sets the variable isJumping in the playerController to true, allowing the player to jump.
        if (other.gameObject.name == ("PlayerChar"))
        {
            GameObject.Find("PlayerChar").GetComponent<slimeMovement>().isJumping = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Once the player exits the JumpCheck, it is set to false and the player can no longer jump.
        if (other.gameObject.name == ("PlayerChar"))
        {
            GameObject.Find("PlayerChar").GetComponent<slimeMovement>().isJumping = false;
        }
    }
}