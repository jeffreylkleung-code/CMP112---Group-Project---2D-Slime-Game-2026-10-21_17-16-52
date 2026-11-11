using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//Add NPC and Dialogue System to your game - Game Code Library
public class NPCScarecrow : MonoBehaviour
{
    public Scarecrow dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerChar")
        {
            if (dialogueData == null && isDialogueActive == false)
            {
                return;
            } 
            if (isDialogueActive)
            {
                NextLine();
            }else
            {
                StartDialogue();
            }
        }
        
    }
    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        nameText.SetText(dialogueData.name);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }
    void NextLine()
    {
        if(isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        } else if(++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);

    }
}
