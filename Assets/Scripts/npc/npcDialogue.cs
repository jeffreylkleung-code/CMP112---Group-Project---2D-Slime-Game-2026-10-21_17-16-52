using UnityEngine;

public class npcDialogue : MonoBehaviour
{
    //ttps://medium.com/@guttermanchad024/creating-a-simple-npc-character-in-unity-47d26086bac1
    [SerializeField] private GameObject dialogue = GameObject.Find("dialogue");
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogue.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("PlayerChar"))
        {
            dialogue.SetActive (true);

        }
           
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("PlayerChar"))
        {
            dialogue.SetActive(false);
        }
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
