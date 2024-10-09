using UnityEngine;

public class DoorButton : MonoBehaviour // Cambié el nombre a DoorButton
{
    public GameObject door; // Referencia a la puerta
    private bool isPressed = false; // Estado del botón
    public AudioClip pressSound; // Clip de audio para el sonido de presión
    private AudioSource audioSource; // Componente AudioSource

    private void Start()
    {
        // Obtener el componente AudioSource del botón
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = pressSound; // Asignar el clip de sonido al AudioSource
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && (other.CompareTag("Player") || other.CompareTag("Bullet")))
        {
            // Acciones a realizar cuando el jugador o una bala colisionan con el botón
            if (door != null)
            {
                door.SetActive(false); // Desactivar la puerta si la referencia no es nula
            }

            audioSource.Play(); // Reproducir el sonido al presionar el botón

            isPressed = true; // Marcar el botón como presionado
            // El botón ya no se destruirá, solo se activará una vez
        }
    }
}

