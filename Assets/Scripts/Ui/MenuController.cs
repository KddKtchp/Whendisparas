using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuController : MonoBehaviour
{
    public void LoadNextScene(string nextScene)
    {
        // Verificar si el nombre de la escena es válido
        if (!string.IsNullOrEmpty(nextScene) && SceneExists(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogError("No se puede cargar la escena: nombre de escena no válido o la escena no se agregó a la configuración de compilación.");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPaused = !EditorApplication.isPaused;
#endif
    }

    // Método para verificar si la escena está en los Build Settings
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
