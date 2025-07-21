using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float playerDistance;
    [SerializeField] private float triggerDistance;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameManager gM;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private bool interiorDoor;
    [SerializeField] private ItemProfile requiredItem;
    [SerializeField] private Inventory inventory;
    bool needItem;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        inventory = gM.GetComponent<Inventory>();
    }

    private void Start()
    {
        if (requiredItem)
        {
            needItem = true;
        }
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.gameObject.transform.position);

        if (player && playerDistance <= triggerDistance)
        {
            if (!needItem)
            {
                if (Input.GetKeyDown(KeyCode.W) && !player.topDownControls)
                {
                    WalkThrough();
                }
                if (player.topDownControls)
                {
                    WalkThrough();
                }
                if (uIManager.doneFadingOut)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            if (needItem && inventory.FindItem(requiredItem))
            {
                if (Input.GetKeyDown(KeyCode.W) && !player.topDownControls)
                {
                    WalkThrough();
                }
                if (player.topDownControls)
                {
                    WalkThrough();
                }
                if (uIManager.doneFadingOut)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }

    private void WalkThrough()
    {
        uIManager.FadeOut();
        if (!interiorDoor)
        {
            gM.GetPlayerPosition();
            gM.FlipFirstLoadBool(false);
        }
    }
}
