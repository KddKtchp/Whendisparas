using UnityEngine;

public class InstantKill : MonoBehaviour
{
    // Referencia al HealthSystem del jugador
    [SerializeField] private HealthSystem playerHealthSystem;

    // Método para matar al jugador instantáneamente
    public void KillPlayer()
    {
        if (playerHealthSystem != null)
        {
            playerHealthSystem.TakeDamage(99999f); // Aplica un daño grande para asegurarse de que la salud llegue a 0
        }
        else
        {
            Debug.LogError("No se ha asignado el HealthSystem del jugador.");
        }
    }

    // Opcional: Método para disparar la muerte instantánea cuando se entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KillPlayer();
        }
    }
}
