using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsumableScript : MonoBehaviour
{
    public Vector3 playerSize;
    public Vector3 initialSize;
    public Vector3 absorbingSize;
    public bool gameOver;
    private float newPlayerSize;
    public GameObject Canvas;
    void Start()
    {
        //At the beginning of the game, find the players current size.
        initialSize = GameObject.Find("PlayerChar").GetComponent<Transform>().localScale;
        //Set the current playerSize to what was just found(the line before is in case we need comparisons)
        playerSize = initialSize;
        gameOver = false;
    }
 
    private void GameOver()
    {
        //If gameOver is ever true (this is due to the enemies), the scene restarts
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           
    }
    //On collisions with the current consumable gameObject then...
    private void OnCollisionEnter2D(Collision2D other)
    {
        //The code only runs if the player collided with them, to avoid issues with collisions with floors, boxes ect.
        if (other.gameObject.name == ("PlayerChar"))
        {
            absorbingSize = gameObject.transform.localScale;
            //The absorbing size is changed depending on the tag, different tags have different sizes, the above line is a failsafe.
            if (gameObject.CompareTag("EnemySpecial"))
            {
                absorbingSize = new Vector3(2, 2, 2);
            } else if (gameObject.CompareTag("FoodSpecial"))
            {
                absorbingSize = new Vector3(0.1f, 0.1f, 0.1f);
            } else if (gameObject.CompareTag("EnemyBoss"))
            {
                absorbingSize = new Vector3(3.0f, 3.0f, 3.0f);
            }
            //If the player is large enough to absorb then...
            if (playerSize.x >= absorbingSize.x || playerSize.y >= absorbingSize.y)
            {
                //This code is learned from a question on stack overflow, it has been learned and adapted throughout the program, however a link to the original is here:
                //https://stackoverflow.com/questions/78504409/how-do-i-change-an-objects-scale-from-another-objects-code-in-unity-2d
                GameObject.Find("PlayerChar").GetComponent<Transform>().localScale += new Vector3(0.2f, 0.2f, 0.2f);
                if (gameObject.CompareTag("EnemyBoss"))
                {
                    //If the boss is eaten, the winScreen is set to being Active, see video in WinScreen.cs for link to the video I learned this in.
                    Canvas.GetComponent<WinScreen>().winScreen.SetActive(true);
                    Time.timeScale = 0;
                }else
                {
                    //If it isnt the boss, the current object is destroyed and the game continous.
                    gameObject.SetActive(false);
                }
                   
            }
            //If the player cannot eat the object...
            else
            {
                //And the object's tag is one of the enemies tags then...
                if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("EnemySpecial") || gameObject.CompareTag("EnemySpecial2"))
                {
                    //Damage the player.
                    GameObject.Find("PlayerChar").GetComponent<Transform>().localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                    playerSize = GameObject.Find("PlayerChar").GetComponent<Transform>().localScale;
                    if (playerSize.x <= 0.2f || playerSize.y <= 0.2f)
                    {
                        //If the player is damaged enough, restart the scene.
                        GameOver();
                    }
                    //Same as above, but boss drains size much faster.
                } else if (gameObject.CompareTag("EnemyBoss"))
                {
                    GameObject.Find("PlayerChar").GetComponent<Transform>().localScale -= new Vector3(0.6f, 0.6f, 0.6f);
                    playerSize = GameObject.Find("PlayerChar").GetComponent<Transform>().localScale;
                    if (playerSize.x <= 0.2f || playerSize.y <= 0.2f)
                    {
                        GameOver();
                    }
                }
            }
        } 
    }
    // Update is called once per frame
    void Update()
    {
        //Updates player size every frame.
        playerSize = GameObject.Find("PlayerChar").GetComponent<Transform>().localScale;
    }
}