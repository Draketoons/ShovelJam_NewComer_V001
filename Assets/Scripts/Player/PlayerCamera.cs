using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float playerDistance;
    [SerializeField] private float distanceLimit;
    [SerializeField] private float smoothingAmmount;
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.gameObject.transform.position);
        if (playerDistance >= distanceLimit)
        {
            if (!player.inside)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.gameObject.transform.position.x, 0, -10), Time.deltaTime * smoothingAmmount);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, -10), Time.deltaTime * smoothingAmmount);
            }
        }
    }
}
