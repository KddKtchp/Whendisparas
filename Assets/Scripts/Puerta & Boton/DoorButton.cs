using UnityEngine;

public class DoorButton : MonoBehaviour // Cambi� el nombre a DoorButton
{
    public GameObject door; // Referencia a la puerta
    private bool isPressed = false; // Estado del bot�n
    public AudioClip pressSound; // Clip de audio para el sonido de presi�n
    private AudioSource audioSource; // Componente AudioSource

    private void Start()
    {
        // Obtener el componente AudioSource del bot�n
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = pressSound; // Asignar el clip de sonido al AudioSource
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && (other.CompareTag("Player") || other.CompareTag("Bullet")))
        {
            // Acciones a realizar cuando el jugador o una bala colisionan con el bot�n
            if (door != null)
            {
                door.SetActive(false); // Desactivar la puerta si la referencia no es nula
            }

            audioSource.Play(); // Reproducir el sonido al presionar el bot�n

            isPressed = true; // Marcar el bot�n como presionado
            // El bot�n ya no se destruir�, solo se activar� una vez
        }
    }
}

