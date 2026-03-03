using UnityEngine;

public class EnemyPatrolChase : MonoBehaviour
{
    public float velocidad = 2f;

    [Header("Persecución")]
    public float radioDeteccion = 5f;

    private Transform jugador;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform objetivoActual;
    private bool persiguiendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaJugador <= radioDeteccion)
        {
            persiguiendo = true;
        }
        else
        {
            persiguiendo = false;
        }

        if (persiguiendo)
        {
            PerseguirJugador();
        }

    }


    void PerseguirJugador()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
        animator.SetBool("isWalking", true);

        if (jugador.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<vida>().PerderVida(1);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}

