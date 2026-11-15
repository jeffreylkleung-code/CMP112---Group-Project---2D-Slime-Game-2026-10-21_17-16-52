using UnityEngine;

public class jumpCheckScript : MonoBehaviour
{
    private BoxCollider2D bc;


    private void Awake()
    {
        bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == ("PlayerChar"))
        {
            GameObject.Find("PlayerChar").GetComponent<slimeMovement>().isJumping = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == ("PlayerChar"))
        {
            GameObject.Find("PlayerChar").GetComponent<slimeMovement>().isJumping = false;
        }
    }
}