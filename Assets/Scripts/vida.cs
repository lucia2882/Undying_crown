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
    public void PerderVida(int cantidad)
    {
        corazones -= cantidad;

        if(corazones <= 2 )
        {
            Destroy(corazon1);
        }
        if(corazones <= 1)
        {
            Destroy(corazon2);
        }

        Debug.Log("Vida restante: " + corazones);

        if (corazones <= 0)
        {
            Destroy(corazon3);
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
}