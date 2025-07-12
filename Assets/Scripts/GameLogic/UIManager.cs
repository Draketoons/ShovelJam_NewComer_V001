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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DisplayString(testText, TEMPtextSpeed);
        }
    }

    public void DisplayString(string textToDisplay, float textSpeed)
    {
        dialogueBox.text = "";
        StopAllCoroutines();
        StartCoroutine(WriteText(textToDisplay, textSpeed));
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
