
using UnityEngine;

public class MeteoritoController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadMinima = 3f;
    public float velocidadMaxima = 7f;
    public float velocidadRotacion = 60f; // Grados por segundo

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Asignamos una velocidad aleatoria hacia abajo
        float velocidad = Random.Range(velocidadMinima, velocidadMaxima);
        rb.linearVelocity = Vector2.down * velocidad;

        // Asignamos una rotación aleatoria (izquierda o derecha)
        float direccionRotacion = Random.value > 0.5f ? 1f : -1f;
        rb.angularVelocity = velocidadRotacion * direccionRotacion;
    }

    // Este método se llamará cuando el meteorito sea destruido
    void OnDestroy()
    {
        // Aquí podrías añadir efectos de explosión, sonido, etc.
        // Por ahora lo dejamos vacío.
    }
}