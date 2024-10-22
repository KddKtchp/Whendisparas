using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Wall : MonoBehaviour
{
    [SerializeField] private Color wallColor = Color.gray; // Color de la muralla
    private Renderer wallRenderer;

    void Start()
    {
        // Asigna el color a la muralla
        wallRenderer = GetComponent<Renderer>();
        if (wallRenderer != null)
        {
            wallRenderer.material.color = wallColor;
        }

        // Asegura que la muralla tenga un collider
        Collider wallCollider = GetComponent<Collider>();
        if (wallCollider == null)
        {
            wallCollider = gameObject.AddComponent<BoxCollider>(); // Añade un BoxCollider si no existe
        }
        wallCollider.isTrigger = false; // Asegura que el collider no sea trigger
    }

    public void SetWallColor(Color newColor)
    {
        // Método para cambiar el color de la muralla dinámicamente
        if (wallRenderer != null)
        {
            wallRenderer.material.color = newColor;
        }
    }
}
