using UnityEngine;

public class LimitarMovimiento : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Se llama al inicio, antes del primer frame
    void Start()
    {
        // Obtenemos la cámara principal. Asegúrate de que tu cámara tenga el tag "MainCamera"
        mainCamera = Camera.main;

        // Calculamos los límites de la pantalla en coordenadas del mundo
        // ViewportToWorldPoint convierte las coordenadas de la pantalla (0 a 1) a coordenadas del mundo
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        
        // Obtenemos el tamaño del objeto para que no se salga ni la mitad
        // Asumimos que la nave tiene un SpriteRenderer. Si es un modelo 3D, usa MeshRenderer.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.extents.x; // La mitad del ancho del objeto
            objectHeight = spriteRenderer.bounds.extents.y; // La mitad del alto del objeto
        }
        else
        {
            // Si no es un sprite, puedes ajustar estos valores manualmente o buscar el MeshRenderer
            Debug.LogWarning("El objeto no tiene SpriteRenderer. Los límites pueden no ser precisos.");
            objectWidth = 0.5f; // Valor por defecto
            objectHeight = 0.5f; // Valor por defecto
        }
    }

    // Se llama después de que todos los Update() han sido ejecutados.
    // Es el mejor lugar para modificar la posición final de un objeto.
    void LateUpdate()
    {
        // Guardamos la posición actual en una variable temporal
        Vector3 viewPos = transform.position;

        // Usamos Mathf.Clamp para limitar los valores de X e Y
        // Restamos el ancho/alto del objeto para que el borde del sprite sea el que toque el límite, no su centro
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        // Actualizamos la posición del objeto con los nuevos valores limitados
        transform.position = viewPos;
    }
}