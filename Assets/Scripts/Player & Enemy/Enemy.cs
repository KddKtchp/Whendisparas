using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f; // Velocidad de movimiento
    [SerializeField] float attackRange = 3f; // Rango de ataque
    [SerializeField] float attackSpeed = 30f; // Velocidad del proyectil
    [SerializeField] GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] Transform firePoint; // Punto desde el que dispara el proyectil
    [SerializeField] float collisionDamage = 10f; // Daño que hace el enemigo al colisionar con el jugador
    [SerializeField] float shootPause = 1;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] bool shootWhenLookingAtPlayer = true;
    [SerializeField] float minimalRotationDistance = 1f;
    float pausedShooting = 0;
    
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        // Moverse hacia el jugador
        if (player)
        {
             
            float distance = Vector3.Distance(transform.position, player.position);
            Quaternion playerRotation = Quaternion.LookRotation(player.position - transform.position);

            Quaternion currentRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, playerRotation.eulerAngles.y, 0), rotationSpeed);
            transform.rotation = currentRotation;

            if (distance < attackRange)
            {
                if (pausedShooting <= 0)
                {
                    if((shootWhenLookingAtPlayer == true && Mathf.Abs(transform.rotation.eulerAngles.y - playerRotation.eulerAngles.y) <= minimalRotationDistance ) || shootWhenLookingAtPlayer == false) { 
                        // Lógica para atacar o disparar
                        Shoot();
                    }
                }
            }
            else { 
                // Mover hacia el jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }

            if (pausedShooting > 0)
            {
                pausedShooting -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil
        GameObject go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * attackSpeed);
        pausedShooting = shootPause;
    }

    // Detecta colisiones con el jugador
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Accede al sistema de salud del jugador y aplica daño
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(collisionDamage);
                Debug.Log("El enemigo ha causado " + collisionDamage + " de daño al jugador.");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(player != null) Gizmos.DrawLine(transform.position, player.position);
    }
}
