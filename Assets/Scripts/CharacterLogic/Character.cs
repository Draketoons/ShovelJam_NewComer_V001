using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterProfile profile;
    [SerializeField] private float talkDistance;
    [SerializeField] private bool drawDebugShapes;
    [SerializeField] private bool talking;
    private UIManager uIManager;
    private DialogueManager dM;
    private PlayerController player;
    private float playerDistance;
    private Animator animator;
    public bool talkedTo;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        dM = GameObject.FindGameObjectWithTag("DM").GetComponent<DialogueManager>();
        animator = GetComponent<Animator>();

        if (profile)
        {
            Debug.Log($"Current Profile: {profile}");
        }
        if (uIManager)
        {
            Debug.Log("UIManager Detected!");
        }
        if (player)
        {
            Debug.Log($"{profile.characterName}: Player Detected!");
        }
        if (dM)
        {
            Debug.Log($"{profile.characterName}: Found DialogueManager!");
        }
    }

    private void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.gameObject.transform.position);

        if (playerDistance < talkDistance)
        {
            dM.currentCharacterObj = this;
            if (!talking)
            {
                dM.currentCharacter = profile;
                dM.currentDialogue = profile.dialogue;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Talk();
                talking = true;
            }
        }
        if (playerDistance >= talkDistance && talking)
        {
            uIManager.CloseDialogueBox();
            uIManager.CloseUpdateUI();
            dM.dialogueIndex = -1;
            dM.currentDialogue = null;
            dM.hasTalkedTo = false;
            dM.checkedItem = false;
            talking = false;
        }
        if (uIManager.doneWriting && animator)
        {
            animator.Play("NPC_Idle");
        }
    }

    private void OnDrawGizmos()
    {
        if (drawDebugShapes)
        {
            Gizmos.DrawWireSphere(transform.position, talkDistance);
        }
    }

    public void PlayTalkAnim()
    {
        if (animator)
        {
            animator.Play("NPC_Talk", 0);
        }
    }

    public void Talk()
    {
        dM.hasTalkedTo = talkedTo;
        dM.currentCharacter = profile;
        dM.AdvanceDialogue();
        if (dM.currentDialogue.response.Count == 0)
        {
            talkedTo = true;
            dM.hasTalkedTo = true;
        }
    }
}
