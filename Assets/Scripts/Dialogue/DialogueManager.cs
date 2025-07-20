using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    [SerializeField] private Inventory inventory;
    public CharacterProfile currentCharacter;
    public Character currentCharacterObj;
    public Dialogue currentDialogue;
    public Response currentResponse;
    public int dialogueIndex;
    public float textWrittingSpeed;
    public bool hasTalkedTo;
    public bool checkedItem;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        inventory = GameObject.FindGameObjectWithTag("GM").GetComponent<Inventory>();
        dialogueIndex = -1;
        hasTalkedTo = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AdvanceDialogue()
    {
        currentCharacterObj.PlayTalkAnim();
        if (inventory.FindItem(currentCharacter.desiredItem) && !checkedItem)
        {
            currentDialogue = currentCharacter.itemDialogue;
            checkedItem = true;
        }
        if (dialogueIndex <= currentDialogue.dialogueText.Length - 1)
        {
            dialogueIndex++;
            if (currentDialogue.response.Count == 0)
            {
                currentCharacterObj.talkedTo = true;
            }
            if (!hasTalkedTo || inventory.FindItem(currentCharacter.desiredItem))
            {
                uIManager.DisplayString(currentDialogue.dialogueText[dialogueIndex], currentCharacter.characterName, textWrittingSpeed);
                if (dialogueIndex >= currentDialogue.dialogueText.Length - 1)
                {
                    uIManager.DisplayResponses();
                    if (currentDialogue.itemToGive)
                    {
                        Debug.Log($"{currentCharacter.characterName} is attempting to give your something...");
                        inventory.AddItemToInventory(currentDialogue.itemToGive);
                    }
                    if (currentDialogue.desiredItem)
                    {
                        inventory.GiveItem(currentCharacter, currentDialogue.desiredItem);
                    }
                }
            }
            if (hasTalkedTo && !checkedItem)
            {
                if (dialogueIndex < currentDialogue.loopingDialogue.Length)
                {
                    uIManager.DisplayString(currentDialogue.loopingDialogue[dialogueIndex], currentCharacter.characterName, textWrittingSpeed);
                }
            }
        }
    }
}
