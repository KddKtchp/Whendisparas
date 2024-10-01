using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] Transform firePoint; // Punto de donde se dispara el proyectil

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
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
