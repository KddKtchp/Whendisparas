using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSceneOnTouch : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player"; // Tag del objeto que activa el cambio de escena

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el Collider tiene el tag correcto
        if (other.CompareTag(targetTag))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Obtener el índice de la escena actual
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Verificar si hay una escena siguiente en la lista de escenas de Unity
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            // Cargar la siguiente escena en la lista
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.LogWarning("No hay más escenas en la lista de compilación.");
        }
    }
}
