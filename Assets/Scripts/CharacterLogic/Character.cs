using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterProfile profile;
    [SerializeField] private float talkDistance;
    [SerializeField] private bool drawDebugShapes;
    [SerializeField] private bool talking;
    [SerializeField] private int dialogueIndex;
    private UIManager uIManager;
    private PlayerController player;
    private float playerDistance;

    private void Awake()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (uIManager)
        {
            Debug.Log("UIManager Detected!");
        }
        if (player)
        {
            Debug.Log($"{profile.characterName}: Player Detected!");
        }
    }

    private void Start()
    {
        dialogueIndex = -1;
    }

    private void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.gameObject.transform.position);

        if (playerDistance < talkDistance)
        {
            if (Input.GetKeyDown(KeyCode.E) && dialogueIndex < profile.dialogue.Length - 1)
            {
                Talk();
                talking = true;
            }
        }
        if (playerDistance >= talkDistance && talking)
        {
            uIManager.CloseDialogueBox();
            dialogueIndex = -1;
            talking = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawDebugShapes)
        {
            Gizmos.DrawWireSphere(transform.position, talkDistance);
        }
    }

    public void Talk()
    {
        dialogueIndex++;
        uIManager.DisplayString(profile.dialogue[dialogueIndex], profile.characterName, 0.03f);
    }
}
