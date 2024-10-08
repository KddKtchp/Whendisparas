using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f; // Velocidad de movimiento
    [SerializeField] float attackRange = 3f; // Rango de ataque
    [SerializeField] float attackSpeed = 30f; // Velocidad del proyectil
    [SerializeField] GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] Transform firePoint; // Punto desde el que dispara el proyectil
    [SerializeField] float collisionDamage = 10f; // Daño que hace el enemigo al colisionar con el jugador

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
    }

    void Update()
    {
        // Moverse hacia el jugador
        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < attackRange)
            {
                // Lógica para atacar o disparar
                Shoot();
            }
            else
            {
                // Mover hacia el jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil
        GameObject go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * attackSpeed);
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
}
