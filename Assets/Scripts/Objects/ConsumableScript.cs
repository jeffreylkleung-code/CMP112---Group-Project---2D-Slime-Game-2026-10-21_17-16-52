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
        initialSize = GameObject.Find("PlayerChar").GetComponent<Transform>().localScale;
        playerSize = initialSize;
        gameOver = false;
    }
 
    private void GameOver()
    {
         Destroy(GameObject.Find("PlayerChar"));
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == ("PlayerChar"))
        {
            absorbingSize = gameObject.transform.localScale;
            if (gameObject.CompareTag("EnemySpecial"))
            {
                absorbingSize = new Vector3(2, 2, 2);
            } else if (gameObject.CompareTag("FoodSpecial"))
            {
                absorbingSize = new Vector3(3.0f, 3.0f, 3.0f);
            } else if (gameObject.CompareTag("EnemyBoss"))
            {
                absorbingSize = new Vector3(0.1f, 0.1f, 0.1f);
            }
            Debug.Log(playerSize.x);
            Debug.Log(absorbingSize.x);
            if (playerSize.x >= absorbingSize.x || playerSize.y >= absorbingSize.y)
            {
                //ttps://stackoverflow.com/questions/78504409/how-do-i-change-an-objects-scale-from-another-objects-code-in-unity-2d
                GameObject.Find("PlayerChar").GetComponent<Transform>().localScale += new Vector3(0.2f, 0.2f, 0.2f);
                if (gameObject.CompareTag("EnemyBoss"))
                {
                    Debug.Log("Boss Eat");
                    Canvas.GetComponent<WinScreen>().winScreen.SetActive(true);
                    //Making an end screen in unity Commenttey
                    Time.timeScale = 0;
                }else
                {
                    Destroy(gameObject);
                }
                   
            }
            else
            {
                if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("EnemySpecial") || gameObject.CompareTag("EnemySpecial2"))
                {
                    GameObject.Find("PlayerChar").GetComponent<Transform>().localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                    playerSize = GameObject.Find("PlayerChar").GetComponent<Transform>().localScale;
                    if (playerSize.x <= 0.2f || playerSize.y <= 0.2f)
                    {
                        GameOver();
                    }
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
        playerSize = GameObject.Find("PlayerChar").GetComponent<Transform>().localScale;
    }
}