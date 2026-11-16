using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == GameObject.Find("PlayerChar").name)
        {
            //If the player enters the box, the scene restarts, this is used for the hole, and also as a failsafe if you fall out the map somehow.
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
