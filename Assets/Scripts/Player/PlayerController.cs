using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private GameManager gM;
    public bool talking;
    public bool topDownControls;

    private void Start()
    {
        currentSpeed = walkSpeed;
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        gM.FindPlayer();
        if (!topDownControls)
        {
            gM.SetPlayerPosition();
        }
    }

    private void Update()
    {
        if (!talking)
        {
            if (!topDownControls)
            {
                Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                transform.Translate(movementVector * currentSpeed * Time.deltaTime);
            }
            else
            {
                Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                transform.Translate(movementVector * currentSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentSpeed = sprintSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                currentSpeed = walkSpeed;
            }
        }
    }
}
