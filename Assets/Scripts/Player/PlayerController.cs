using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    public float currentSpeed;

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.Translate(movementVector * currentSpeed * Time.deltaTime);
    }
}
