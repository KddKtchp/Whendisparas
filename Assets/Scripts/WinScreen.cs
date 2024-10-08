using UnityEngine;
using UnityEngine.UI; // Para trabajar con elementos de UI

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject uiElement; // El elemento de UI que queremos mostrar
    [SerializeField] private string targetTag = "Player"; // Tag del objeto que debe activar la UI

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
            // Mostrar el UI si colisiona con el objeto correcto
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ocultar el UI cuando el objeto sale del Collider
        if (other.CompareTag(targetTag))
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }

    }
}
