using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI nameBox;
    [SerializeField] private TextMeshProUGUI response1Text, response2Text;
    [SerializeField] private string testText;
    [SerializeField] private float TEMPtextSpeed;
    [Header("UI")]
    [SerializeField] private Button responseButton1, responseButton2;
    [SerializeField] private GameObject dialogueBoxUI;
    [SerializeField] private Image blackScreen;
    [SerializeField] private GameObject clockHand;
    [Header("GameLogic")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DayTimer dayTimer;
    [SerializeField] private Camera currentCamera;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private bool fadingOut;
    [SerializeField] private bool fadingIn;
    public float t;

    private void Start()
    {
        CloseDialogueBox();
        t = 1.0f;
        FadeIn();
    }

    public void DisplayString(string textToDisplay, string nameText, float textSpeed)
    {
        OpenDialogueBox();
        dialogueBox.text = "";
        nameBox.text = nameText;
        StopAllCoroutines();
        StartCoroutine(WriteText(textToDisplay, textSpeed));
    }

    private void Update()
    {
        if (fadingOut)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.Lerp(0.0f, 1.0f, t));
            t +=  fadeSpeed * Time.deltaTime;
            if (t >= 2.0f)
            {
                fadingOut = false;
            }
        }
        if (fadingIn)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.Lerp(0.0f, 1.0f, t));
            t -= fadeSpeed * Time.deltaTime;
            if (t <= 0.0f)
            {
                fadingIn = false;
            }
        }
        clockHand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, dayTimer.percentage * 360.0f));
    }

    public void DisplayResponses()
    {
        dialogueManager.dialogueIndex = -1;
        switch (dialogueManager.currentDialogue.response.Count)
        {
            case 0:
                break;
            case 1:
                responseButton2.gameObject.SetActive(true);
                response2Text.text = dialogueManager.currentDialogue.response[0].repsonseText;
                responseButton2.onClick.AddListener(() => SetCurrentDialogue(dialogueManager.currentDialogue.response[0].nextDialogue));
                break;
            case 2:
                responseButton1.gameObject.SetActive(true);
                response1Text.text = dialogueManager.currentDialogue.response[0].repsonseText;
                responseButton1.onClick.AddListener(() => SetCurrentDialogue(dialogueManager.currentDialogue.response[0].nextDialogue));
                responseButton2.gameObject.SetActive(true);
                response2Text.text = dialogueManager.currentDialogue.response[1].repsonseText;
                responseButton2.onClick.AddListener(() => SetCurrentDialogue(dialogueManager.currentDialogue.response[1].nextDialogue));
                break;
        }
    }

    public void CloseResponses()
    {
        responseButton1.gameObject.SetActive(false);
        responseButton1.onClick.RemoveAllListeners();
        responseButton2.gameObject.SetActive(false);
        responseButton2.onClick.RemoveAllListeners();
    }

    public void OpenDialogueBox()
    {
        CloseResponses();
        dialogueBoxUI.SetActive(true);
        if (currentCamera.WorldToViewportPoint(dialogueManager.currentCharacterObj.transform.position).y > 0.5f)
        {
            dialogueBoxUI.transform.position = new Vector3(dialogueBox.transform.position.x, -50.0f, dialogueBox.transform.position.z);
            Debug.Log("Current character is up");
        }
        if (currentCamera.WorldToViewportPoint(dialogueManager.currentCharacterObj.transform.position).y < 0.5f)
        {
            dialogueBoxUI.transform.position = new Vector3(dialogueBox.transform.position.x, 500.0f, currentCamera.transform.position.z);
            Debug.Log("Current character is Down");
        }
        Debug.Log($"Current character screen y position: {currentCamera.WorldToViewportPoint(dialogueManager.currentCharacterObj.transform.position).y}");
    }

    public void SetCurrentDialogue(Dialogue dialogueToSet)
    {
        dialogueManager.currentDialogue = dialogueToSet;
        dialogueManager.AdvanceDialogue();
    }

    public void FadeOut()
    {
        fadingOut = true;
    }

    public void FadeIn()
    {
        fadingIn = true;
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
