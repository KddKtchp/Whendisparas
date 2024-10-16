using UnityEngine;

public class InstantKill : MonoBehaviour
{
    // Referencia al HealthSystem del jugador
    [SerializeField] private HealthSystem playerHealthSystem;

    // M�todo para matar al jugador instant�neamente
    public void KillPlayer()
    {
        if (playerHealthSystem != null)
        {
            playerHealthSystem.TakeDamage(99999f); // Aplica un da�o grande para asegurarse de que la salud llegue a 0
        }
        else
        {
            Debug.LogError("No se ha asignado el HealthSystem del jugador.");
        }
    }

    // Opcional: M�todo para disparar la muerte instant�nea cuando se entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KillPlayer();
        }
    }
}
