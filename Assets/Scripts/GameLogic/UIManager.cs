using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI loopCounterText;
    [SerializeField] private TextMeshProUGUI updateText;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private string testText;
    public bool doneWriting;
    [Header("UI")]
    [SerializeField] private Button responseButton1, responseButton2;
    [SerializeField] private GameObject dialogueBoxUI;
    [SerializeField] private Image blackScreen;
    [SerializeField] private GameObject clockHand;
    [SerializeField] private GameObject updateUI;
    [SerializeField] private GameObject inventoryUI;
    [Header("GameLogic")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DayTimer dayTimer;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Camera currentCamera;
    [SerializeField] private GameManager gM;
    [SerializeField] private float fadeSpeed;
    [Header("Animation")]
    [SerializeField] private Animator UIAnimator;
    public bool doneFadingOut;
    public bool interior;

    private void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        dayTimer = GameObject.FindGameObjectWithTag("GM").GetComponent<DayTimer>();
        inventory = GameObject.FindGameObjectWithTag("GM").GetComponent<Inventory>();
        UIAnimator = GetComponent<Animator>();
        inventory.FindUIManager();
        dayTimer.FindUIManager();
        dayTimer.FindAudioManager();
        dayTimer.paused = false;
        dayTimer.CheckCurrentTime();
        CloseDialogueBox();
        FadeIn();
        doneFadingOut = false;
        if (gM.startingNewDay)
        {
            loopCounterText.text = $"Loop: {gM.loopCount}";
            PlayLoopCountAnim();
            gM.startingNewDay = false;
        }
    }

    public void DisplayString(string textToDisplay, string nameText, float textSpeed)
    {
        OpenDialogueBox();
        dialogueBox.text = "";
        nameBox.text = nameText;
        StopAllCoroutines();
        StartCoroutine(WriteText(textToDisplay, textSpeed));
    }

    private void FixedUpdate()
    {
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

    public void PlayLoopCountAnim()
    {
        UIAnimator.Play("LoopCounterAnim");
    }

    public void FadeIn()
    {
        UIAnimator.Play("FadeIn");
    }

    public void FadeOut()
    {
        UIAnimator.Play("FadeOut");
    }

    public void FlipDoneFadingOutBool()
    {
        doneFadingOut = true;
    }

    public Image GetBlackScreen()
    {
        return blackScreen;
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

    public void CloseDialogueBox()
    {
        dialogueBox.text = "";
        nameBox.text = "";
        dialogueBoxUI.SetActive(false);
    }

    public void OpenUpdateUI()
    {
        updateUI.SetActive(true);
    }

    public void CloseUpdateUI()
    {
        updateUI.SetActive(false);
    }

    public void OpenInventoryUI()
    {
        inventoryUI.SetActive(true);
    }

    public void CloseInventoryUI()
    {
        inventoryUI.SetActive(false);
    }

    public void DisplayInventory(List<ItemProfile> itemList)
    {
        OpenInventoryUI();
        string inventoryString = "";
        int count = 0;
        foreach (ItemProfile item in itemList)
        {
            count++;
            inventoryString += $" {item.itemName} ";
            if (count == 6)
            {
                inventoryString += "\n";
                count = 0;
            }
        }
        inventoryText.text = inventoryString;
    }

    public void DisplayUpdateText(string textToDisplay)
    {
        updateText.text = textToDisplay;
    }

    public void SetTimeText(int time, string aMPM)
    {
        timeText.text = $"{time}:{aMPM}";
    }

    IEnumerator WriteText(string textToWrite, float writeSpeed)
    {
        doneWriting = false;
        foreach (char c in textToWrite.ToCharArray())
        {
            dialogueBox.text += c;
            if (c == ' ')
            {
                yield return new WaitForSeconds(0.0f);
            }
            yield return new WaitForSeconds(writeSpeed);
        }
        doneWriting = true;
    }
}
