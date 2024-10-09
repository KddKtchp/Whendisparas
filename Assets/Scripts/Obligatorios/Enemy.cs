using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float attackSpeed = 30f;
    [SerializeField] float attackCooldown = 2f; // Tiempo entre disparos
    [SerializeField] GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] Transform firePoint; // Punto de donde se dispara el proyectil

    private Transform player;
    private float lastAttackTime; // Para controlar el cooldown

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
        lastAttackTime = -attackCooldown; // Permitir disparar inmediatamente al iniciar
    }

    void Update()
    {
        // Moverse hacia el jugador
        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            // Debug.Log(distance);
            if (distance < attackRange)
            {
                // Verificar si el cooldown ha terminado
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Shoot(); // Lógica para atacar o disparar
                    lastAttackTime = Time.time; // Actualizar el tiempo del último disparo
                }
            }
            else
            {
                // Mover hacia el jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            Vector3 dir = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);


        }
    }

    void Shoot()
    {
        // Instanciar el proyectil
        GameObject go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * attackSpeed);
    }
}
