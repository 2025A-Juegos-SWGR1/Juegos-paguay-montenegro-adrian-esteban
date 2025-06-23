using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // --- NUEVO: Necesario para gestionar escenas

public class GameManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    public static GameManager instance;

    // --- Referencias de UI ---
    [Header("UI y Puntuación")]
    public TMP_Text puntajeTexto;
    
    // --- NUEVO: Referencias para la pantalla de Game Over ---
    [Header("Pantalla de Game Over")]
    public GameObject panelGameOver; // Arrastra aquí tu PanelGameOver
    public TMP_Text textoPuntajeFinal; // Arrastra aquí tu TextoPuntajeFinal

    // --- Referencias de Audio ---
    [Header("Configuración de Audio")]
    public AudioClip musicaDeFondo;
    public AudioClip sonidoDeMuerte;
    private AudioSource audioSource;

    // --- Variables de Estado del Juego ---
    private float tiempoSobrevivido;
    private bool jugadorVivo = true;

    private void Awake()
    {
        // ... (código del Singleton y AudioSource sin cambios)
        if (instance == null) { instance = this; } else { Destroy(gameObject); }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { audioSource = gameObject.AddComponent<AudioSource>(); }
    }

    private void Start()
    {
        // --- NUEVO: Nos aseguramos de que el panel esté oculto al empezar ---
        panelGameOver.SetActive(false);
        // --- NUEVO: Nos aseguramos de que el tiempo corra normalmente al inicio ---
        Time.timeScale = 1f;

        // ... (código para iniciar la música sin cambios)
        if (musicaDeFondo != null) { audioSource.clip = musicaDeFondo; audioSource.loop = true; audioSource.Play(); }
    }

    void Update()
    {
        if (jugadorVivo)
        {
            tiempoSobrevivido += Time.deltaTime;
            puntajeTexto.text = "Puntaje: " + tiempoSobrevivido.ToString("F0");
        }
    }

    public void JugadorHaMuerto()
    {
        if (!jugadorVivo) return; // Evita que se llame múltiples veces

        jugadorVivo = false;
        puntajeTexto.text += ""; // Ocultamos el puntaje en tiempo real si quieres

        // --- NUEVO: Lógica de Game Over ---
        
        // 1. Pausamos el juego para que todo se detenga
        Time.timeScale = 0f;

        // 2. Mostramos el panel de Game Over
        panelGameOver.SetActive(true);

        // 3. Actualizamos el texto del puntaje final en el panel
        textoPuntajeFinal.text = "Puntaje Final: " + tiempoSobrevivido.ToString("F0");
    }
    
    // --- NUEVO: Función pública para el botón de reinicio ---
    public void ReiniciarJuego()
    {
        // 1. Reanudamos el tiempo antes de cargar la escena
        Time.timeScale = 1f;

        // 2. Recargamos la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // ... (función ReproducirSonidoMuerte sin cambios)
    public void ReproducirSonidoMuerte()
    {
        if (sonidoDeMuerte != null) { audioSource.PlayOneShot(sonidoDeMuerte); }
    }
}
