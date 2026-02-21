using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class enemigoscuervo : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animacion;
    private Rigidbody2D rb;

    void Start()
    {
        GetComponent<cuervo>().enabled = true;       
        currentHealth = maxHealth;
        rb=GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo herido! Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            EnemyPatrolChase scriptMovimiento = GetComponent<EnemyPatrolChase>();
            scriptMovimiento.enabled = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
     
        yield return new WaitForSeconds(0.1f);
// Busca el script llamado "NombreDeTuScript" y lo apaga
        GetComponent<cuervo>().enabled = false;       
         animacion.SetTrigger("muerte");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Enemigo derrotado");
        Destroy(gameObject);
 // O activa una animación de muerte
    }
}