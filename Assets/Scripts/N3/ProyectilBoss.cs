using UnityEngine;

public class ProyectilBoss : MonoBehaviour
{
    public float velocidad = 10f;
    public int daño = 1;
    public float tiempoVida = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, tiempoVida);
        
        if (rb != null)
        {
            // Movimiento horizontal basado en la rotación
            rb.linearVelocity = transform.right * velocidad;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            vida v = collision.GetComponent<vida>();
            if (v != null) v.PerderVida(daño);
            Destroy(gameObject); 
        }
    }
} 