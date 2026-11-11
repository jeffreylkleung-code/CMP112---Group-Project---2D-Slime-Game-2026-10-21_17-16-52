using UnityEngine;

[CreateAssetMenu(fileName = "Scarecrow", menuName = "Scriptable Objects/Scarecrow")]
public class Scarecrow : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;

}
