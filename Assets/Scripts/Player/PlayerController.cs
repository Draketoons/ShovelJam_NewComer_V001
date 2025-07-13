using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    [SerializeField] private float currentSpeed;
    public bool talking;

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        if (!talking)
        {
            Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), 0);
            transform.Translate(movementVector * currentSpeed * Time.deltaTime);
        }
    }
}
