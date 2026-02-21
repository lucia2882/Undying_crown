using UnityEngine;
using UnityEngine.SceneManagement;

public class final : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
void OnTriggerEnter2D(Collider2D other)
{
    if(other.CompareTag("Player"))
    {
        SceneManager.LoadScene("Final");
    }
}
}
