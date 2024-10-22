using UnityEngine;
using UnityEngine.UI; // Si usas un componente de UI, como Text o Panel.

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox; // Caja de di�logo que aparecer�
    [SerializeField] private string targetTag = "Player"; // Etiqueta del jugador para detectar colisi�n
    [SerializeField] private float detectionRadius = 3f; // Radio de detecci�n alrededor del objeto
    private Transform playerTransform; // Transform del jugador para medir la distancia

    void Start()
    {
        // Aseg�rate de que la caja de di�logo est� oculta al inicio
        dialogueBox.SetActive(false);

        // Encuentra al jugador por su etiqueta (tag)
        GameObject player = GameObject.FindGameObjectWithTag(targetTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No se encontr� un objeto con la etiqueta 'Player' en la escena.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calcula la distancia entre el jugador y este GameObject
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Muestra la caja de di�logo si el jugador est� dentro del radio de detecci�n
            if (distance <= detectionRadius)
            {
                ShowDialogue();
            }
            else
            {
                HideDialogue();
            }
        }
    }

    void ShowDialogue()
    {
        // Activar la caja de di�logo
        dialogueBox.SetActive(true);
    }

    void HideDialogue()
    {
        // Desactivar la caja de di�logo
        dialogueBox.SetActive(false);
    }

    // Opcional: M�todo para cambiar el radio de detecci�n din�micamente
    public void SetDetectionRadius(float newRadius)
    {
        detectionRadius = newRadius;
    }
}
