using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class vida : MonoBehaviour
{
    public int corazones = 3;
    public Animator muerte;
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;
    void Start()
    {
        corazones=3;
        corazon1.SetActive(true);
        corazon2.SetActive(true);
        corazon3.SetActive(true);
    }
    public void PerderVida(int cantidad)
    {
        corazones -= cantidad;

        if(corazones <= 2 )
        {
        corazon1.SetActive(false);
        }
        if(corazones <= 1)
        {
        corazon2.SetActive(false);
        }

        Debug.Log("Vida restante: " + corazones);

        if (corazones <= 0)
        {
            corazon3.SetActive(false);
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Jugador muerto");
        if (muerte != null) muerte.SetTrigger("muerte");
        StartCoroutine(Cargarmenu());
    }
    IEnumerator Cargarmenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("menu");
    }
public void recuperarcorazon()
{
    // Solo curar si nos falta vida
    if (corazones < 3)
    {
        corazones++; // Aumentamos el contador de vida

        // Lógica escalonada:
        // Si el corazón 3 está apagado, lo encendemos y salimos (return)
        if (corazon3.activeSelf == false) 
        {
            corazon3.SetActive(true);
            return; // Importante para que no siga encendiendo los demás
        }

        // Si el 3 estaba bien, probamos con el 2
        if (corazon2.activeSelf == false) 
        {
            corazon2.SetActive(true);
            return;
        }

        // Si el 2 estaba bien, probamos con el 1
        if (corazon1.activeSelf == false) 
        {
            corazon1.SetActive(true);
            return;
        }
    }
}
}