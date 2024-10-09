using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Movement speed
    [SerializeField] float jumpForce = 5f; // Jump force
    [SerializeField] LayerMask groundLayer; // Layer to define what is ground
    [SerializeField] Transform groundCheck; // Empty GameObject to check if player is grounded
    [SerializeField] float groundCheckRadius = 0.2f; // Radius for ground checking
    [SerializeField] float customGravity = -20f; // Custom gravity (negative for downward force)
    [SerializeField] float fallMultiplier = 2.5f; // Multiplier for when falling down
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 velocity; // To track vertical velocity

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable default gravity to use custom gravity
    }

    void Update()
    {
        Move();
        Jump();
        ApplyCustomGravity(); // Apply gravity every frame

        if (Time.timeScale == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + (new Vector3(transform.forward.x, 0, transform.forward.z)).normalized * moveZ;
        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset downward velocity when grounded
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * customGravity);
        }
    }

    void ApplyCustomGravity()
    {
        // Apply custom gravity if not grounded
        if (!isGrounded)
        {
            velocity.y += customGravity * fallMultiplier * Time.deltaTime;
        }

        // Apply vertical velocity to the Rigidbody
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
}
