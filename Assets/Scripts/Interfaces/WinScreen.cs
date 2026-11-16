using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    //Code was directly adapted from the video "Making an end screen in Unity" by Commettey
    //Link to video: https://www.youtube.com/watch?v=6zHyq0TqFuk
    public GameObject winScreen;

    public void Retry()
    {
        //Restarts scene by resetting the timscale back from zero, the scene would be paused before this function is called
        Time.timeScale = 1.0f;
        //Restarts current scene- In this case "Main Game"
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
}
