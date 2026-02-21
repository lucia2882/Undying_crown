using UnityEngine;

public class cuervo : MonoBehaviour

{
    public float velocidad = 2f;
    public Transform puntoA;

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
    transform.position = Vector2.MoveTowards(transform.position, puntoA.position, velocidad * Time.deltaTime);
    animator.SetBool("isWalking", true);   
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

