using UnityEngine;
using TMPro;

public class DetectCollisions : MonoBehaviour
{
    static int puntaje = 0;
    static TextMeshProUGUI texto;

    private void OnTriggerEnter(Collider other)
    {
         if (!other.CompareTag("Comida")) return;
        if (!gameObject.activeSelf) return;

        if (texto == null)
            texto = FindFirstObjectByType<TextMeshProUGUI>();

        puntaje++;
        texto.text = puntaje.ToString();

        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
    }
}