using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    [SerializeField] private float currentSpeed;
    public bool talking;
    public bool topDownControls;

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        if (!talking)
        {
            if (!topDownControls)
            {
                Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), 0);
                transform.Translate(movementVector * currentSpeed * Time.deltaTime);
            }
            else
            {
                Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
