using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private GameManager gM;
    [SerializeField] private Vector2 movementVector;
    public bool talking;
    public bool topDownControls;
    public bool inside;
    public bool ladderMode;
    Animator animator;

    private void Start()
    {
        currentSpeed = walkSpeed;
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        gM.FindPlayer();
        if (!inside)
        {
            gM.SetPlayerPosition();
        }
    }

    private void Update()
    {
        if (!talking)
        {
            movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (!topDownControls && !ladderMode)
            {
                transform.Translate(new Vector3(movementVector.x, 0.0f, 0.0f) * currentSpeed * Time.deltaTime);
            }
            if (ladderMode)
            {
                transform.Translate(new Vector3(0.0f, movementVector.y, 0.0f) * currentSpeed * Time.deltaTime);
                if (movementVector.y != 0.0f)
                {
                    animator.Play("Player_WalkUp");
                }
            }
            else if (topDownControls && !ladderMode)
            {
                transform.Translate(movementVector * currentSpeed * Time.deltaTime);
                if (movementVector.y == 1.0f)
                {
                    animator.Play("Player_WalkUp");
                }
                if (movementVector.y == -1.0f)
                {
                    animator.Play("Player_WalkDown");
                }
            }

            if (movementVector.x != 0.0f && movementVector.y == 0.0f && !ladderMode)
            {
                animator.Play("Player_Walk");
                if (movementVector.x == 1.0f)
                {
                    transform.localScale = new Vector3(4.42f, 6.38f, 1);
                }
                if (movementVector.x == -1.0f)
                {
                    transform.localScale = new Vector3(-4.42f, 6.38f, 1);
                }
            }
            else if (movementVector.x == 0.0f && movementVector.y == 0.0f)
            {
                animator.Play("Player_Idle");
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
