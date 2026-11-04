using UnityEngine;

public class ConsumableScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == ("PlayerChar"))
        {
            //ttps://stackoverflow.com/questions/78504409/how-do-i-change-an-objects-scale-from-another-objects-code-in-unity-2d
            GameObject.Find("PlayerChar").GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Destroy(gameObject);

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}