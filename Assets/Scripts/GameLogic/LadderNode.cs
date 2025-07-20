using UnityEngine;

public class LadderNode : MonoBehaviour
{
    public float triggerDistance;
    [SerializeField] private PlayerController player;
    [SerializeField] private float playerDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.gameObject.transform.position);
        if (playerDistance <= triggerDistance)
        {
            player.ladderMode = !player.ladderMode;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}
