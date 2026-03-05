using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class vida : MonoBehaviour
{
    public int corazones = 3;
    public int vidaMaxima = 3; // Nueva variable para controlar el límite
    public Animator muerte;

    [Header("UI Corazones")]
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;
    public GameObject corazon4; // Nuevo slot para el cuarto corazón

    void Start()
    {
        // Al empezar, nos aseguramos de que solo se vean los corazones iniciales
        ActualizarVisualizacion();
    }

    public void PerderVida(int cantidad)
    {
        corazones -= cantidad;

        // Lógica de apagado (del más alto al más bajo)
        if (corazones < 4) corazon4.SetActive(false);
        if (corazones < 3) corazon3.SetActive(false);
        if (corazones < 2) corazon2.SetActive(false);
        if (corazones < 1) corazon1.SetActive(false);

        if (corazones <= 0)
        {
            Morir();
        }
    }

    // Nueva función para cuando recoges el ÍTEM de Corazón (Sube el máximo a 4)
    public void AumentarVidaMaxima()
    {
        vidaMaxima = 4;
        corazones = 4;
        ActualizarVisualizacion();
    }

    public void recuperarcorazon()
    {
        if (corazones < vidaMaxima)
        {
            corazones++;
            ActualizarVisualizacion();
        }
    }

    // Función auxiliar para encender/apagar corazones según el número actual
    void ActualizarVisualizacion()
{
    if (corazon1 != null) corazon1.SetActive(corazones >= 1);
    if (corazon2 != null) corazon2.SetActive(corazones >= 2);
    if (corazon3 != null) corazon3.SetActive(corazones >= 3);
    
    // El truco está aquí: solo intenta usar corazon4 si NO es nulo
    if (corazon4 != null) 
    {
        if(vidaMaxima == 4) 
            corazon4.SetActive(corazones >= 4);
        else
            corazon4.SetActive(false);
    }
}

    void Morir()
    {
        if (muerte != null) muerte.SetTrigger("muerte");
        StartCoroutine(Cargarmenu());
    }

    IEnumerator Cargarmenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("menu");
    }
}