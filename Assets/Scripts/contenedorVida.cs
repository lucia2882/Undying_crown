using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class contenedorVida : MonoBehaviour
{
    public List<GameObject> listaCorazones; 
    public int vidaMaxima = 3;

    // EL ERROR SUELE ESTAR AQUÍ: No llames a funciones de UI aquí arriba.
    
    void Start()
    {
        // Esto se ejecuta en el hilo principal, es seguro.
        ActualizarUI(vidaMaxima); 
    }

    public void ActualizarUI(int vidaMaxima)
    {
        // Validamos que la lista no esté vacía para evitar otros errores
        if (listaCorazones == null || listaCorazones.Count == 0) return;

        for (int i = 0; i < listaCorazones.Count; i++)
        {
            if (i < vidaMaxima)
            {
                listaCorazones[i].SetActive(true);
            }
            else
            {
                listaCorazones[i].SetActive(false);
            }
        }
    }
}