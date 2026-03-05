using UnityEngine;

public class EnemyPatrolChase : MonoBehaviour
{
    public float velocidad = 2f;

    [Header("Persecución")]
    public float radioDeteccion = 5f;

    [Header("Ataque (Opcional)")]
    public float tiempoEntreAtaques = 1.5f;
    private float cronometroAtaque;

    private Transform jugador;
    private Rigidbody2D rb;
    private Animator animator;
    private vida scriptVidaJugador;
    private ataque_distancia scriptAtaque; // Declaración añadida
    private bool persiguiendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scriptAtaque = GetComponent<ataque_distancia>(); // Busca si tiene script de disparo
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            jugador = playerObj.transform;
            scriptVidaJugador = playerObj.GetComponent<vida>();
        }
    } 

    void Update()
    {
        if (jugador == null || (scriptVidaJugador != null && scriptVidaJugador.corazones <= 0))
        {
            persiguiendo = false;
            if (animator != null) animator.SetBool("isWalking", false);
            return;
        }

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaJugador <= radioDeteccion)
        {
            persiguiendo = true;
        }
        else
        {
            persiguiendo = false;
            if (animator != null) animator.SetBool("isWalking", false);
        }

        if (persiguiendo)
        {
            PerseguirJugador();

            if (scriptAtaque != null) 
            {
                cronometroAtaque += Time.deltaTime;
                if (cronometroAtaque >= tiempoEntreAtaques)
                {
                    Atacar();
                    cronometroAtaque = 0;
                }
            }
        }
    }

    void Atacar()
    {
        if (scriptAtaque != null)
        {
            scriptAtaque.Disparar(jugador);
        }
    }

    void PerseguirJugador()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
        if (animator != null) animator.SetBool("isWalking", true);

        if (jugador.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vida v = collision.gameObject.GetComponent<vida>();
            if (v != null) v.PerderVida(1);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}