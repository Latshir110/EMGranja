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

    void OnEnable()
    {
        tiempoEsquivando = 0;
        ultimaComida = null;
    }
    void Update()
    {

        if (tiempoEsquivando > 0)
        {
            transform.Translate(Vector3.right * direccion * velocidadEsquive * Time.deltaTime);
            tiempoEsquivando -= Time.deltaTime;
            return;
        }

    
        foreach (Collider c in Physics.OverlapSphere(transform.position, radioDeteccion))
        {
            if (c.CompareTag("Comida") && c != ultimaComida)
            {
                ultimaComida = c; 

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