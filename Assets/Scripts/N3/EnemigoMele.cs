using UnityEngine;

public class EnemigoMele : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public float rangoAtaque = 1.5f;
    public float tiempoEntreAtaques = 2f;
    private float cronometroAtaque;

    [Header("Referencias")]
    public Animator animator;
    public Transform controladorGolpe; // Un objeto vacío en la punta de la guadaña
    public float radioGolpe = 0.5f;
    public int dañoAtaque = 1;

    private Transform jugador;
    private bool puedeAtacar = true;
    private EnemyPatrolChase scriptMovimiento;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) jugador = playerObj.transform;
        if (animator == null) animator = GetComponent<Animator>();
        scriptMovimiento = GetComponent<EnemyPatrolChase>();
    }
    
void Update()
{
    if (jugador == null) return;

    float distancia = Vector2.Distance(transform.position, jugador.position);

    // El cronómetro debe sumar SIEMPRE si el script está activo
    cronometroAtaque += Time.deltaTime;

    if (distancia <= rangoAtaque && cronometroAtaque >= tiempoEntreAtaques)
    {
        Atacar();
        cronometroAtaque = 0;
    }
}

    void Atacar()
    {
       if (animator != null)
    {
        animator.SetTrigger("ataque");
    
        if (scriptMovimiento != null)
        {
            scriptMovimiento.enabled = false; 
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
        Invoke("ReanudarMovimiento", 1.2f); 
    }
    }

    // Esta función la llamaremos DESDE la animación (Evento de Animación)
    public void GolpeMele()
    {
        Collider2D[] objetosGolpeados = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D col in objetosGolpeados)
        {
            if (col.CompareTag("Player"))
            {
                vida v = col.GetComponent<vida>();
                if (v != null) v.PerderVida(dañoAtaque);
            }
        }
    }
    void ReanudarMovimiento()
    {   
    if (scriptMovimiento != null)
        {
        scriptMovimiento.enabled = true; // El enemigo vuelve a perseguirte
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
        if(controladorGolpe != null) Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}