using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == GameObject.Find("PlayerChar").name)
        {
            Destroy(GameObject.Find("PlayerChar"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
