using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private PlayerController player;
    [SerializeField] private Camera playerCam;
    public Vector3 previousPlayerPosition;
    public Vector3 previousCameraPosition;
    public Vector3 startingPosition;
    public int loopCount;
    public bool startingNewDay;
    [SerializeField] bool firstLoad;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        firstLoad = true;
    }

    public void SetPlayerPosition()
    {
        if (!firstLoad && !startingNewDay)
        {
            player.gameObject.transform.position = previousPlayerPosition;
            playerCam.transform.position = previousCameraPosition;
            firstLoad = false;
        }
        if (startingNewDay)
        {
            player.gameObject.transform.position = startingPosition;
            playerCam.transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y + 5, -10);
        }
    }

    public void FlipFirstLoadBool(bool b)
    {
        firstLoad = b;
    }

    public void GetPlayerPosition()
    {
        previousPlayerPosition = player.gameObject.transform.position;
        previousCameraPosition = playerCam.transform.position;
        Debug.Log($"Previous player position: {previousPlayerPosition}\n Previous cam position: {previousCameraPosition}");
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
