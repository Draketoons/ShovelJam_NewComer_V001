using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private string testText;
    [SerializeField] private float TEMPtextSpeed;
    [SerializeField] private GameObject dialogueBoxUI;

    private void Start()
    {
        CloseDialogueBox();
    }

    public void DisplayString(string textToDisplay, string nameText, float textSpeed)
    {
        OpenDialogueBox();
        dialogueBox.text = "";
        nameBox.text = nameText;
        StopAllCoroutines();
        StartCoroutine(WriteText(textToDisplay, textSpeed));
    }

    public void OpenDialogueBox()
    {
        dialogueBoxUI.SetActive(true);
    }

    public void CloseDialogueBox()
    {
        dialogueBox.text = "";
        nameBox.text = "";
        dialogueBoxUI.SetActive(false);
    }

    IEnumerator WriteText(string textToWrite, float writeSpeed)
    {
        foreach (char c in textToWrite.ToCharArray())
        {
            dialogueBox.text += c;
            if (c == ' ')
            {
                yield return new WaitForSeconds(0.0f);
            }
            yield return new WaitForSeconds(writeSpeed);
        }
    }
}
