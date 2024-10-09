using UnityEngine;

public class Door : MonoBehaviour
{
    // Aquí puedes agregar lógica para la puerta, como abrir y cerrar, si es necesario

    // Ejemplo: Método para abrir la puerta
    public void OpenDoor()
    {
        // Lógica para abrir la puerta
        gameObject.SetActive(false); // Esto desactivará la puerta
    }

    // Si necesitas más funcionalidad, puedes agregarla aquí.
}
