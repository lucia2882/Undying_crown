using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class vida : MonoBehaviour
{
    public int corazones = 3;
    public Animator muerte;
    
    public void PerderVida(int cantidad)
    {
        corazones -= cantidad;
        Debug.Log("Vida restante: " + corazones);

        if (corazones <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Jugador muerto");
        muerte.SetTrigger("muerte");
        SceneManager.LoadScene("menu");
    }
}
