using UnityEngine;

public class PlanetaController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadMinima = 1f;
    public float velocidadMaxima = 3f;

    [Header("Configuración de Rotación")]
    public float velocidadRotacionMinima = 5f;
    public float velocidadRotacionMaxima = 20f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Asignamos una velocidad aleatoria hacia abajo (más lenta que los meteoritos)
        float velocidad = Random.Range(velocidadMinima, velocidadMaxima);
        rb.linearVelocity = Vector2.down * velocidad;

        // Asignamos una rotación aleatoria y lenta
        float velocidadRotacion = Random.Range(velocidadRotacionMinima, velocidadRotacionMaxima);
        float direccionRotacion = Random.value > 0.5f ? 1f : -1f; // Decide si rota a la derecha o izquierda
        rb.angularVelocity = velocidadRotacion * direccionRotacion;
    }

    // Esta función se llama automáticamente cuando el objeto ya no es visible por ninguna cámara.
    // Es perfecto para destruir objetos que ya salieron de la pantalla.
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
