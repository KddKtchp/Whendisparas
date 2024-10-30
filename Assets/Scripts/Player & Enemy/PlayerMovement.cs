using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Velocidad de movimiento
    [SerializeField] float jumpForce = 5f; // Fuerza de salto
    [SerializeField] LayerMask groundLayer; // Capa para definir el suelo
    [SerializeField] Transform groundCheck; // Objeto vacío para verificar si el jugador está en el suelo
    [SerializeField] float groundCheckRadius = 0.2f; // Radio de verificación de suelo
    [SerializeField] float customGravity = -20f; // Gravedad personalizada
    [SerializeField] float fallMultiplier = 2.5f; // Multiplicador al caer
    [SerializeField] private GameObject gameOverUI; // UI de Game Over
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivar la gravedad predeterminada para usar la personalizada
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // Asegurar que la UI de Game Over esté oculta al inicio
        }
    }

    void Update()
    {
        if (gameOverUI != null && gameOverUI.activeSelf) return; // Evitar que el jugador se mueva si el menú de Game Over está activo

        Move();
        Jump();
        ApplyCustomGravity();

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
            velocity.y = -2f; // Reiniciar la velocidad hacia abajo cuando está en el suelo
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * customGravity);
        }
    }

    void ApplyCustomGravity()
    {
        if (!isGrounded)
        {
            velocity.y += customGravity * fallMultiplier * Time.deltaTime;
        }
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    // Llamar a esta función cuando el jugador pierda
    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Mostrar la UI de Game Over
            Time.timeScale = 0; // Pausar el juego
            Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor
            Cursor.visible = true; // Hacer visible el cursor
        }
    }
}
