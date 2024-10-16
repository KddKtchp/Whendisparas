using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // Para trabajar con elementos de UI
using UnityEngine.SceneManagement; // Para manejar cambios de escena

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject uiElement; // El elemento de UI que queremos mostrar
    [SerializeField] private string targetTag = "Player"; // Tag del objeto que debe activar la UI
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    private void Start()
    {
        // Asegurarse de que el UI esté oculto al inicio
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el Collider tiene el tag correcto
        if (other.CompareTag(targetTag))
        {
            Time.timeScale = 0; // Congelar el tiempo cuando se gana
            onTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ocultar el UI cuando el objeto sale del Collider
        if (other.CompareTag(targetTag))
        {
            onTriggerExit?.Invoke();
        }
    }

    // Este método puede ser llamado al hacer clic en el botón "Siguiente nivel" o "Reiniciar"
    public void LoadNextScene(string sceneName)
    {
        Time.timeScale = 1; // Restaurar el tiempo antes de cargar la siguiente escena
        SceneManager.LoadScene(sceneName);
    }
}
