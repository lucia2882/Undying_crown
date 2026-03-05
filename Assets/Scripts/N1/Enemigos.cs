using UnityEngine;
using System.Collections;

public class Enemigos : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animacion;
    private Rigidbody2D rb;

public bool IsDead() 
{
    return currentHealth <= 0;
}

    void Start()
    {
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
        if(scriptMovimiento != null)
        {
            scriptMovimiento.enabled = false;
        }
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
     
        yield return new WaitForSeconds(0.1f);
        animacion.SetTrigger("muerte");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Enemigo derrotado");
        Destroy(gameObject);
    }
}