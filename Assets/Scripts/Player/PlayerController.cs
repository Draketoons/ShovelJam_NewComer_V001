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
    Animator animator;

    private void Start()
    {
        currentSpeed = walkSpeed;
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
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
            Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (!topDownControls)
            {
                
                transform.Translate(new Vector3(movementVector.x, 0.0f, 0.0f) * currentSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(movementVector * currentSpeed * Time.deltaTime);
            }

            if (movementVector.x != 0.0f || movementVector.y != 0.0f)
            {
                animator.Play("Player_Walk");
                if (movementVector.x == 1.0f)
                {
                    transform.localScale = new Vector3(4.42f, 6.38f, 1);
                    Debug.Log("Going Right");
                }
                if (movementVector.x == -1.0f)
                {
                    transform.localScale = new Vector3(-4.42f, 6.38f, 1);
                    Debug.Log("Going Left");
                }
                Debug.Log($"movementVector: {movementVector}");
            }
            else
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
