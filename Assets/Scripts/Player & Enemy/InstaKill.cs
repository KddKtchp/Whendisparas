using UnityEngine;
using UnityEngine.Events;

public class InstaKillSystem : MonoBehaviour
{
    [SerializeField] private UnityEvent onDeath; // Evento que se llama al morir
    [SerializeField] private GameObject deathUI; // UI para la pantalla de muerte
    [SerializeField] private string targetTag = "Player"; // Tag del jugador para detectar colisión

    void Start()
    {
        // Asegurarse de que la UI de muerte esté desactivada al inicio
        if (deathUI != null)
        {
            deathUI.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto que colisiona tiene el tag del jugador
        if (collision.gameObject.CompareTag(targetTag))
        {
            InstaKill();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger tiene el tag del jugador
        if (other.CompareTag(targetTag))
        {
            InstaKill();
        }
    }

    public void InstaKill()
    {
        // Ejecuta el evento de muerte y pausa el juego, excepto la UI
        onDeath?.Invoke();
        PausarJuegoPorMuerte();
    }

    void PausarJuegoPorMuerte()
    {
        // Pausa el juego
        Time.timeScale = 0;

        // Muestra la UI de muerte
        if (deathUI != null)
        {
            deathUI.SetActive(true);
        }

        // Desbloquea y muestra el cursor para interactuar con la UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        // Reanuda el juego
        Time.timeScale = 1;

        // Oculta la UI de muerte
        if (deathUI != null)
        {
            deathUI.SetActive(false);
        }

        // Bloquea y oculta el cursor nuevamente (si es necesario después de revivir)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
