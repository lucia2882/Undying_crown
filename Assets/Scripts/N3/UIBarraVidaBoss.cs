using UnityEngine;
using UnityEngine.UI; // Necesario para usar 'Image' normal
// using TMPro; // Descomenta esto si usas TextMeshPro para el nombre

public class UIBarraVidaBoss : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelBarraVida; // El objeto padre "BarraVidaBoss"
    public Image imagenRelleno; // La imagen "Relleno" con Filled activo
    // public TMP_Text textoNombreBoss; // Descomenta si usas TextMeshPro

    [Header("Configuración (Solo visual)")]
    public float velocidadSuavizado = 5f; // Cómo de rápido baja la barra (más suave)
    private float fillObjetivo = 1f;

    // Singleton para acceder fácil desde el Boss
    public static UIBarraVidaBoss instance;

    void Awake()
    {
        // Configuración del Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Empezamos con la barra oculta hasta que aparezca el Boss
        DesactivarBarra();
    }

    void Update()
    {
        // Suavizamos el movimiento de la barra para que no baje de golpe
        imagenRelleno.fillAmount = Mathf.Lerp(imagenRelleno.fillAmount, fillObjetivo, velocidadSuavizado * Time.deltaTime);
    }

    // --- Funciones Públicas ---

    public void ActivarBarra(string nombreBoss, int vidaMaxima)
    {
        if (panelBarraVida != null)
        {
            panelBarraVida.SetActive(true);
            // Si usas TextMeshPro, aquí pones el nombre:
            // if(textoNombreBoss != null) textoNombreBoss.text = nombreBoss;
            
            // Reiniciamos la barra al 100%
            fillObjetivo = 1f;
            imagenRelleno.fillAmount = 1f;
        }
    }

    // Esta es la función que llamará el Boss cuando reciba daño
    public void ActualizarVida(int vidaActual, int vidaMaxima)
    {
        // Calculamos el porcentaje (entre 0 y 1)
        fillObjetivo = (float)vidaActual / vidaMaxima;
    }

    public void DesactivarBarra()
    {
        if (panelBarraVida != null)
        {
            panelBarraVida.SetActive(false);
        }
    }
}