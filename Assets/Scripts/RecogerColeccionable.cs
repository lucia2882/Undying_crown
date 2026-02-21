using UnityEngine;
using TMPro; // Librería para TextMeshPro
using UnityEngine.UI; // Librería para Texto Legacy

public class RecogerColeccionable : MonoBehaviour
{
    public static int contador = 0;

    // OPCIÓN A: Si tu texto es TextMeshPro, usa esta variable:
    public TextMeshProUGUI textoTMP;

    // OPCIÓN B: Si tu texto es Legacy, usa esta variable:
    public Text textoLegacy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contador++;
            ActualizarInterfaz();
            Destroy(gameObject);
        }
    }

    void ActualizarInterfaz()
    {
        // El código detectará cuál de las dos variables has rellenado
        if (textoTMP != null)
        {
            textoTMP.text = "Items: " + contador;
        }
        
        if (textoLegacy != null)
        {
            textoLegacy.text = "Items: " + contador;
        }
    }
}