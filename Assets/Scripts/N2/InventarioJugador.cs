using UnityEngine;
using UnityEngine.UI;

public class InventarioJugador : MonoBehaviour
{
    [Header("Configuración")]
    public bool tieneMochila = false;
    public int objetosActuales = 0;
    public int limiteSinMochila = 2;
    public int limiteConMochila = 4;

    [Header("UI del Canvas")]
    public GameObject[] imagenesUI; // Arrastra aquí las 4 imágenes del Canvas
    public GameObject objetoTP; // Referencia interna para el objeto TP

    public bool PuedeRecoger()
    {
        int limite = tieneMochila ? limiteConMochila : limiteSinMochila;
        return objetosActuales < limite;
    }

    public void RecogerObjeto(int indiceImagen)
    {
        objetosActuales++;
        if (indiceImagen < imagenesUI.Length)
        {
            imagenesUI[indiceImagen].SetActive(true); // Muestra la imagen en el Canvas
        }
    }
}