using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class botone : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {        
        startButton.onClick.AddListener(Empezarjuego);
        quitButton.onClick.AddListener(Salirjuego);
    }

    // Update is called once per frame
public void Empezarjuego()
{
    SceneManager.LoadScene("Nivel 1");
}
public void Salirjuego()
{
    Application.Quit();
    Debug.Log("Cerrado");
}
}
