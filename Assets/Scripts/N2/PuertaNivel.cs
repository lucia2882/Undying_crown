using UnityEngine;
using TMPro; // Para el mensaje de texto

public class PuertaNivel : MonoBehaviour
{
    public TextMeshProUGUI textoMensaje; // Arrastra el texto del Canvas aquí
    public GameObject imagenTP; // La imagen del TP que está en el Canvas

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verificamos si la imagen del TP en el Canvas está activa
            if (imagenTP.activeSelf)
            {
                textoMensaje.text = "¡Nivel Completado! Llevas el TP.";
                textoMensaje.color = Color.green;
                // Aquí podrías añadir: SceneManager.LoadScene("SiguienteNivel");
            }
            else
            {
                textoMensaje.text = "No puedes salir sin el TP";
                textoMensaje.color = Color.red;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Limpiar el mensaje cuando el jugador se aleje
        if (collision.gameObject.CompareTag("Player"))
        {
            textoMensaje.text = "";
        }
    }
}