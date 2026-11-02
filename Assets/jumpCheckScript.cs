using UnityEngine;

public class jumpCheckScript : MonoBehaviour
{
    private BoxCollider2D bc;
    public bool isJumping = true;

    private void Awake()
    {
        bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == ("PlayerChar"))
        {
            isJumping = true;
            Debug.Log("In object");
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == ("PlayerChar"))
        {
            isJumping = false;
            Debug.Log("Out Object");
        }
    }
}