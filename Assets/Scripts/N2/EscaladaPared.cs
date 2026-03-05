using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EscaladaPared : MonoBehaviour
{
    [Header("Ajustes de Velocidad")]
    public float velocidadEscalada = 5f;
    public float tiempoMaximoEscalada = 2f;
    public float impulsoBorde = 4f; // Fuerza para saltar sobre la esquina

    [Header("Detección (Capa: Ground)")]
    public LayerMask capaGround; // Selecciona "Ground"
    public float distanciaPared = 0.5f; 
    public float distanciaSuelo = 1.2f;

    private Rigidbody2D rb;
    private float tiempoRestante;
    private bool estaEscalando;
    private bool puedeEscalar = true;
    private float gravedadOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravedadOriginal = rb.gravityScale;
        tiempoRestante = tiempoMaximoEscalada;
    }

    void Update()
    {
        float dirX = transform.localScale.x > 0 ? 1 : -1;
        Vector2 mirando = new Vector2(dirX, 0);

        // Raycast para pared y suelo
        bool tocandoPared = Physics2D.Raycast(transform.position, mirando, distanciaPared, capaGround);
        bool enSuelo = Physics2D.Raycast(transform.position, Vector2.down, distanciaSuelo, capaGround);

        if (enSuelo)
        {
            tiempoRestante = tiempoMaximoEscalada;
            puedeEscalar = true;
        }

        float inputV = Input.GetAxisRaw("Vertical");

        if (tocandoPared && inputV > 0 && puedeEscalar && tiempoRestante > 0)
        {
            ComenzarEscalada(inputV);
        }
        else
        {
            // SI ESTABA ESCALANDO Y SE ACABA LA PARED O EL TIEMPO
            if (estaEscalando)
            {
                // Aplicamos un impulso hacia arriba y hacia adelante
                rb.linearVelocity = new Vector2(dirX * impulsoBorde, impulsoBorde);
            }
            DetenerEscalada();
        }
    }

    void ComenzarEscalada(float v)
    {
        estaEscalando = true;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(0, v * velocidadEscalada);
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0) puedeEscalar = false;
    }

    void DetenerEscalada()
    {
        estaEscalando = false;
        rb.gravityScale = gravedadOriginal;
    }
}