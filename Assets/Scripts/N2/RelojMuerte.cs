using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class RelojMuerte : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    public float tiempoRestante = 60f; // Tiempo inicial en segundos
    public TextMeshProUGUI textoReloj; // Arrastra aquí el texto de la UI

    private vida scriptVida;
    private bool jugadorMuerto = false;

    void Start()
    {
        // Buscamos el script de vida en el jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            scriptVida = jugador.GetComponent<vida>();
        }
    }

    void Update()
    {
        if (jugadorMuerto) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarDisplay(tiempoRestante);
        }
        else
        {
            tiempoRestante = 0;
            ActualizarDisplay(tiempoRestante);
            MatarJugador();
        }
    }

    void ActualizarDisplay(float tiempo)
    {
        // Formatea el tiempo en Minutos:Segundos
        float minutos = Mathf.FloorToInt(tiempo / 60);
        float segundos = Mathf.FloorToInt(tiempo % 60);
        textoReloj.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // Opcional: poner el texto en rojo si queda poco tiempo
        if (tiempo < 10f) textoReloj.color = Color.red;
    }

    void MatarJugador()
    {
        if (scriptVida != null)
        {
            jugadorMuerto = true;
            // Llamamos a tu función PerderVida con un daño letal
            scriptVida.PerderVida(99); 
        }
    }
}