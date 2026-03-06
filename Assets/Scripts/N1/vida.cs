using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class vida : MonoBehaviour
{
    public int corazones = 3;
    public int vidaMaxima = 3; 
    public Animator muerte;

    [Header("UI Corazones")]
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;
    public GameObject corazon4; // En el Nivel 1, deja esto vacío (None)

    void Start()
    {
        ActualizarVisualizacion();
    }

    public void PerderVida(int cantidad)
    {
        corazones -= cantidad;

        // VERIFICACIÓN DE SEGURIDAD: Solo intentamos apagar si el objeto existe
        if (corazon1 != null) corazon1.SetActive(corazones >= 1);
        if (corazon2 != null) corazon2.SetActive(corazones >= 2);
        if (corazon3 != null) corazon3.SetActive(corazones >= 3);
        if (corazon4 != null) corazon4.SetActive(corazones >= 4);

        if (corazones <= 0)
        {
            Morir();
        }
    }

    public void AumentarVidaMaxima()
    {
        // Solo aumentamos si realmente tenemos donde mostrarlo
        if (corazon4 != null) 
        {
            vidaMaxima = 4;
            corazones = 4;
            ActualizarVisualizacion();
        }
        else 
        {
            Debug.LogWarning("No has asignado el objeto Corazon 4 en el Inspector.");
        }
    }

    public void recuperarcorazon()
    {
        if (corazones < vidaMaxima)
        {
            corazones++;
            ActualizarVisualizacion();
        }
    }

    void ActualizarVisualizacion()
    {
        // El operador != null evita el error de "MissingReferenceException" o "NullReference"
        if (corazon1 != null) corazon1.SetActive(corazones >= 1);
        if (corazon2 != null) corazon2.SetActive(corazones >= 2);
        if (corazon3 != null) corazon3.SetActive(corazones >= 3);
        
        if (corazon4 != null) 
        {
            // Solo se muestra si la vida máxima es 4 Y tenemos 4 corazones
            corazon4.SetActive(vidaMaxima == 4 && corazones >= 4);
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