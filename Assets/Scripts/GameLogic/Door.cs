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

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.gameObject.transform.position);

        if (player && playerDistance <= triggerDistance)
        {
            if (Input.GetKeyDown(KeyCode.W) && !player.topDownControls)
            {
                WalkThrough();
            }
            if (player.topDownControls)
            {
                WalkThrough();
            }
        }
        if (uIManager.t >= 2.0f)
        {
            SceneManager.LoadScene(sceneToLoad);
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
