using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // The projectile to instantiate
    [SerializeField] Transform firePoint; // The point from where the projectile is shot
    [SerializeField] float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] float fireRate = 0.5f; // Time between shots
    [SerializeField] float bulletLife = 1;
    [SerializeField] float rocketJumpForce = 10f; // The force applied for rocket jump
    [SerializeField] LayerMask groundLayer; // Layer to define what is ground
    [SerializeField] Transform groundCheck; // Ground check reference
    [SerializeField] float groundCheckRadius = 0.2f; // Ground check radius
    [SerializeField] int maxAirShots = 2; // Maximum number of shots allowed while in the air

    private float nextFireTime = 0f;
    private Rigidbody playerRb;
    private bool isGrounded;
    private bool wasGrounded; // To track if the player just landed
    private int airShotCounter = 0; // Tracks the number of shots made while in the air

    void Start()
    {
        // Get the Rigidbody component of the player
        playerRb = GetComponent<Rigidbody>();
    }

   void Update()
{
    // Check if the player is grounded
    isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

    // Debug output
    Debug.Log($"Is Grounded: {isGrounded}, Air Shots: {airShotCounter}, Can Shoot: {CanShoot()}");

    // Reset air shot counter when landing on the ground
    if (isGrounded)
    {
        airShotCounter = 0;
    }

    // Update wasGrounded to track if the player just landed
    wasGrounded = isGrounded;

    // Shooting logic
    if (Input.GetButton("Fire1") && Time.time > nextFireTime && CanShoot())
    {
        Shoot();
        nextFireTime = Time.time + fireRate;
    }
}


    void Shoot()
    {
        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the projectile and apply force to it
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        // Destroy the projectile after a set amount of time
        Destroy(projectile, bulletLife);

        // If the player is not grounded, apply rocket jump force and increment air shot counter
        if (!isGrounded)
        {
            Vector3 rocketJumpDirection = -firePoint.forward;
            playerRb.AddForce(rocketJumpDirection * rocketJumpForce, ForceMode.Impulse);
            airShotCounter++; // Increment the air shot counter
        }
    }

    bool CanShoot()
    {
        // Allow shooting if the player is grounded or if air shots are available
        return isGrounded || airShotCounter < maxAirShots;
    }
}
