using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    public CharacterProfile currentCharacter;
    public Character currentCharacterObj;
    public Dialogue currentDialogue;
    public Response currentResponse;
    public int dialogueIndex;
    public float textWrittingSpeed;
    public bool hasTalkedTo;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
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
        if (dialogueIndex <= currentDialogue.dialogueText.Length - 1)
        {
            dialogueIndex++;
            if (currentDialogue.response.Count == 0)
            {
                currentCharacterObj.talkedTo = true;
            }
            if (!hasTalkedTo)
            {
                uIManager.DisplayString(currentDialogue.dialogueText[dialogueIndex], currentCharacter.characterName, textWrittingSpeed);
                if (dialogueIndex >= currentDialogue.dialogueText.Length - 1)
                {
                    uIManager.DisplayResponses();
                }
            }
            if (hasTalkedTo)
            {
                if (dialogueIndex < currentDialogue.loopingDialogue.Length)
                {
                    uIManager.DisplayString(currentDialogue.loopingDialogue[dialogueIndex], currentCharacter.characterName, textWrittingSpeed);
                }
            }
        }
    }
}
