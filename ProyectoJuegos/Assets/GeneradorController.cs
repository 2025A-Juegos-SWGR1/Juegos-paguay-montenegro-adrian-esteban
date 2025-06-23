using System.Collections;
using UnityEngine;

public class GeneradorController : MonoBehaviour
{
    [Header("Prefabs a Generar")]
    public GameObject meteoritoPrefab;
    public GameObject planetaPrefab;

    [Header("Configuración de Meteoritos")]
    public float intervaloSpawnMeteorito = 1.5f;

    [Header("Configuración de Planetas")]
    public float intervaloSpawnPlaneta = 10f;
    [Range(0, 1)]
    public float probabilidadSpawnPlaneta = 0.2f;

    [Header("Área de Spawn")]
    public float rangoHorizontal = 9f;

    void Start()
    {
        // Asegurémonos de que las rutinas se inician
        Debug.Log("Iniciando Spawner...");
        if (meteoritoPrefab != null) StartCoroutine(SpawnearObjetos(meteoritoPrefab, intervaloSpawnMeteorito));
        if (planetaPrefab != null) StartCoroutine(SpawnearObjetos(planetaPrefab, intervaloSpawnPlaneta, true));
    }

    IEnumerator SpawnearObjetos(GameObject prefab, float intervalo, bool esPlaneta = false)
    {
        yield return new WaitForSeconds(2f); // Espera inicial
        Debug.Log(">>> Iniciando bucle de spawn para: " + prefab.name, prefab);

        while (true)
        {
            yield return new WaitForSeconds(intervalo);
            Debug.Log("Intentando spawn de: " + prefab.name);

            if (esPlaneta)
            {
                float dado = Random.value;
                Debug.Log("Es un planeta. Probabilidad requerida: <= " + probabilidadSpawnPlaneta + ". Tirada de dado: " + dado);
                if (dado > probabilidadSpawnPlaneta)
                {
                    Debug.Log("...FALLÓ la tirada de probabilidad. No se genera planeta esta vez.");
                    continue;
                }
                Debug.Log("%c...¡ÉXITO en la tirada! Generando planeta." );
            }

            float posicionXAleatoria = Random.Range(-rangoHorizontal, rangoHorizontal);
            Vector3 posicionDeSpawn = new Vector3(posicionXAleatoria, transform.position.y, 0);

            Instantiate(prefab, posicionDeSpawn, Quaternion.identity);
            Debug.Log("Instanciado un " + prefab.name + " en la posición " + posicionDeSpawn);
        }
    }
}