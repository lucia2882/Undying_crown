using UnityEngine;
using TMPro;

public class BarreraJefe : MonoBehaviour
{
    [Header("Requisitos")]
    public int enemigosNecesarios = 5;
    private int enemigosMuertos = 0;

    [Header("Referencias")]
    public GameObject objetoColumna; 
    public GameObject mensajeUI;
    public TextMeshProUGUI textoMensaje;

    private bool puertaAbierta = false;
    private bool jugadorYaEntro = false;

    void Start()
    {
        if (mensajeUI != null) mensajeUI.SetActive(false);
        if (objetoColumna != null) objetoColumna.SetActive(true);
    }

    public void EnemigoDerrotado()
    {
        enemigosMuertos++;
        if (enemigosMuertos >= enemigosNecesarios && !puertaAbierta)
        {
            AbrirPuerta();
        }
    }

    void AbrirPuerta()
    {
        puertaAbierta = true;
        if (objetoColumna != null) objetoColumna.SetActive(false); 
        Debug.Log("Paso abierto.");
    }

    // --- EL TEXTO ---
    // Usamos Collision en lugar de Trigger para el mensaje si quieres que sea al tocar
    // Pero como el script está en el Padre Trigger, detectaremos la cercanía
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !puertaAbierta)
        {
            // Solo mostramos mensaje si el jugador está "pegado" a la izquierda de la columna
            if (other.transform.position.x < transform.position.x)
            {
                MostrarMensaje("Derrota a los aldeanos para pasar...");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OcultarMensaje();

            // LÓGICA DE CIERRE SEGURA:
            // Si la puerta está abierta y el jugador sale por la DERECHA del trigger
            // (Asegúrate de que el trigger sea ancho para que esto se detecte bien)
            float posicionJugador = other.transform.position.x;
            float centroTrigger = transform.position.x;

            if (puertaAbierta && !jugadorYaEntro && posicionJugador > centroTrigger + 1f) 
            {
                CerrarPuerta();
            }
        }
    }

    void CerrarPuerta()
    {
        jugadorYaEntro = true;
        if (objetoColumna != null) objetoColumna.SetActive(true);
        // Desactivamos el script para que no vuelva a abrirse ni dar mensajes
        this.enabled = false; 
        Debug.Log("Puerta cerrada para siempre.");
    }

    void MostrarMensaje(string frase)
    {
        if (mensajeUI != null)
        {
            mensajeUI.SetActive(true);
            if (textoMensaje != null) textoMensaje.text = frase;
        }
    }

    void OcultarMensaje()
    {
        if (mensajeUI != null) mensajeUI.SetActive(false);
    }
}