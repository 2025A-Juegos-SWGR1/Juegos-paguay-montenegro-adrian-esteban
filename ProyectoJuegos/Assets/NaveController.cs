using UnityEngine;
using UnityEngine.InputSystem;

public class NaveController : MonoBehaviour
{
    // --- Variables de Movimiento y Rotación ---
    [Header("Configuración de Movimiento")]
    public float velocidadDeMovimiento = 5f;

    [Header("Configuración Visual")]
    public float velocidadDeRotacion = 10f;
    public float anguloMaximoInclinacion = 15f;
    public float velocidadDeEscala = 10f;
    public float factorEscalaVertical = 0.1f; // 0.1 = 10% de cambio

    // --- Componentes ---
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;

    // --- Variables de Estado ---
    private Vector2 direccionMovimiento;
    private Vector3 escalaOriginal;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movimiento"];
        
        // Guardamos la escala con la que empieza la nave
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        // Leemos el input del jugador
        direccionMovimiento = moveAction.ReadValue<Vector2>();

        // Aplicamos los efectos visuales de rotación y escala
        ManejarInclinacionVisual();
    }

    void FixedUpdate()
    {
        // Aplicamos el movimiento físico
        rb.MovePosition(rb.position + direccionMovimiento.normalized * velocidadDeMovimiento * Time.fixedDeltaTime);
    }

    void ManejarInclinacionVisual()
    {
        // --- ROTACIÓN EN EJE Z (Izquierda/Derecha) ---
        float horizontalInput = direccionMovimiento.x;
        float anguloObjetivoZ = -horizontalInput * anguloMaximoInclinacion;
        Quaternion rotacionObjetivo = Quaternion.Euler(0, 0, anguloObjetivoZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadDeRotacion * Time.deltaTime);

        // --- ESCALA EN EJE Y (Arriba/Abajo) para simular profundidad ---
        float verticalInput = direccionMovimiento.y;
        float escalaObjetivoY = escalaOriginal.y + (escalaOriginal.y * verticalInput * factorEscalaVertical);
        Vector3 escalaObjetivo = new Vector3(escalaOriginal.x, escalaObjetivoY, escalaOriginal.z);
        transform.localScale = Vector3.Lerp(transform.localScale, escalaObjetivo, velocidadDeEscala * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteorito") || collision.gameObject.CompareTag("Planeta"))
        {
            Debug.Log("¡Colisión fatal! Chocaste con: " + collision.gameObject.name);

            // --- NUEVO: Le pedimos al GameManager que reproduzca el sonido de muerte ---
            GameManager.instance.ReproducirSonidoMuerte();

            // Le avisamos al GameManager que la nave fue destruida.
            GameManager.instance.JugadorHaMuerto();

            // Aquí puedes añadir una animación de explosión
        
            Destroy(gameObject); // Destruye la nave
        }
    }
}