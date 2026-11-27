using UnityEngine;

public class EsquivarComida : MonoBehaviour
{
    public float radioDeteccion = 6f;
    public float velocidadEsquive = 8f;
    public float duracionEsquive = 0.4f;
    [Range(0, 1)] public float probabilidad = 0.5f;

    float tiempoEsquivando = 0;
    float direccion = 0;
    Collider ultimaComida;

    // Reinicia el estado cada vez que el animal sale de la pool
    void OnEnable()
    {
        tiempoEsquivando = 0;
        ultimaComida = null;
    }

    void Update()
    {
        // Estado Esquivar: se mueve de lado hasta que se acabe el tiempo
        if (tiempoEsquivando > 0)
        {
            transform.Translate(Vector3.right * direccion * velocidadEsquive * Time.deltaTime);
            tiempoEsquivando -= Time.deltaTime;
            return;
        }

        // Estado Avanzar: percibe la comida cercana dentro del radio
        foreach (Collider c in Physics.OverlapSphere(transform.position, radioDeteccion))
        {
            if (c.CompareTag("Comida") && c != ultimaComida) // comida nueva
            {
                ultimaComida = c; // la recuerda para decidir una sola vez

                // Decide al azar si esquiva y hacia qué lado
                if (Random.value < probabilidad)
                {
                    direccion = Random.value < 0.5f ? -1 : 1;
                    tiempoEsquivando = duracionEsquive;
                }
                break;
            }
        }
    }
}