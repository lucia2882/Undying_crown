using UnityEngine;
using System.Collections;

public class FinalBoss : MonoBehaviour
{
    public enum FaseBoss { Fase1, Fase2 }

    [Header("Estado del Boss")]
    public FaseBoss faseActual = FaseBoss.Fase1;
    public int maxHealth = 300;
    private int currentHealth;
    public bool estaMuerto = false;

    [Header("Movimiento Vertical")]
    public float velocidadVertical = 3f;
    public float alturaMinima = 0f;
    public float alturaMaxima = 10f;
    public float margenPersecucion = 0.5f;

    [Header("Configuración de Ataques")]
    public float tiempoEntreAtaques = 3f;
    private float cronometroAtaques;
    public float distanciaDeteccionAtaque = 12f;
    private bool atacando = false;

    [Header("Proyectiles (Prefabs)")]
    public GameObject rayoPrefab;      // El rayo de Fase 1
    public GameObject bolaFuegoPrefab; // La bola de Fase 2

    [Header("Referencias")]
    public Transform firePointBolas;   // El farolillo (origen de todo)
    public Animator animator;
    public Transform jugador;
    private vida scriptVidaJugador;
    private Rigidbody2D rb;

[Header("Victoria")]
public GameObject mensajeVictoria; // Arrastra aquí el objeto TextoVictoria
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        
        // Configuración física para que sea un Boss volador estable
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic; 
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        // Buscar al jugador si no está asignado
        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) jugador = playerObj.transform;
        }

        if (jugador != null) scriptVidaJugador = jugador.GetComponent<vida>();
        if (animator == null) animator = GetComponent<Animator>();

        faseActual = FaseBoss.Fase1;
        cronometroAtaques = 0; 

        if (UIBarraVidaBoss.instance != null)
        {
            UIBarraVidaBoss.instance.ActivarBarra("Boss", maxHealth);
        }
    }

    void Update()
    {
        // No hacer nada si está muerto, atacando o el jugador murió
        if (estaMuerto || jugador == null || atacando || (scriptVidaJugador != null && scriptVidaJugador.corazones <= 0)) return;

        PerseguirJugadorVerticalmente();

 // 3. Sistema de Ataques con detección de distancia
    float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

    if (distanciaAlJugador <= distanciaDeteccionAtaque)
    {
        // El cronómetro SOLO sube si NO estamos ya en medio de un ataque
        if (!atacando) 
        {
            cronometroAtaques += Time.deltaTime;
        }

        if (cronometroAtaques >= tiempoEntreAtaques)
        {
            // Verificamos altura
            if (Mathf.Abs(jugador.position.y - transform.position.y) < 2f) 
            {
                cronometroAtaques = 0; // RESETEAMOS AQUÍ MISMO antes de empezar la corrutina
                StartCoroutine(SecuenciaAtaque());
            }
        }
    }
    else 
    {
        // Opcional: Si el jugador se aleja, el cronómetro se pausa o se resetea
        cronometroAtaques = 0; 
    }
        GirarHaciaJugador();
    }

    // --- LÓGICA DE ATAQUE POR FASES ---
    IEnumerator SecuenciaAtaque()
    {
        atacando = true; 
        
        if (faseActual == FaseBoss.Fase1)
        {
            // FASE 1: Un solo rayo potente
            animator.SetTrigger("ataqueRayo");
            yield return new WaitForSeconds(1.5f); // Espera a que termine la animación
        }
        else
        {
            // FASE 2: Ráfaga de 3 bolas rápidas
            for (int i = 0; i < 3; i++) 
            {
                animator.SetTrigger("ataqueFarol");
                // El proyectil se crea mediante el Animation Event (SpawnBolaFuego)
                yield return new WaitForSeconds(0.4f); // Tiempo entre disparos de la ráfaga
            }
            yield return new WaitForSeconds(1f); // Tiempo de recuperación tras la ráfaga
        }
        
        atacando = false; 
    }

    // --- MOVIMIENTO Y ORIENTACIÓN ---
    void PerseguirJugadorVerticalmente()
    {
        float diferenciaY = jugador.position.y - transform.position.y;

        if (Mathf.Abs(diferenciaY) > margenPersecucion)
        {
            float nuevaY = Mathf.MoveTowards(transform.position.y, jugador.position.y, velocidadVertical * Time.deltaTime);
            nuevaY = Mathf.Clamp(nuevaY, alturaMinima, alturaMaxima);
            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
        }
    }

    void GirarHaciaJugador()
    {
        if (jugador.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // --- RECIBIR DAÑO Y CAMBIO DE FASE ---
    public void TakeDamage(int damage)
    {
        if (estaMuerto) return;

        currentHealth -= damage;
        
        if (UIBarraVidaBoss.instance != null)
            UIBarraVidaBoss.instance.ActualizarVida(currentHealth, maxHealth);

        if (faseActual == FaseBoss.Fase1 && currentHealth <= maxHealth / 2)
            CambiarAFase2();

        if (currentHealth <= 0)
            StartCoroutine(Die());
    }

    void CambiarAFase2()
    {
        faseActual = FaseBoss.Fase2;
        tiempoEntreAtaques *= 0.7f; // Ataca más seguido
        velocidadVertical *= 1.4f; // Se mueve más rápido

        IEnumerator FlashRojo()
{
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    if (sr != null)
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;
    }
}
        
        if (animator != null) animator.SetTrigger("cambioFase");
        Debug.Log("BOSS EN FASE 2: ¡RÁFAGAS!");
    }

    // --- EVENTOS DE ANIMACIÓN (Deben estar en los clips de ataque) ---

    public void SpawnRayo() 
    {
        if (rayoPrefab == null || firePointBolas == null) return;

        Quaternion rotacion = transform.localScale.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        Instantiate(rayoPrefab, firePointBolas.position, rotacion);
    }

    public void SpawnBolaFuego()
    {
        if (bolaFuegoPrefab == null || firePointBolas == null) return;

        Quaternion rotacion = transform.localScale.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        Instantiate(bolaFuegoPrefab, firePointBolas.position, rotacion);
    }

    // --- MUERTE ---
    IEnumerator Die()
    {
        estaMuerto = true;
        if (UIBarraVidaBoss.instance != null) UIBarraVidaBoss.instance.DesactivarBarra();

    if (mensajeVictoria != null)
        {
            mensajeVictoria.SetActive(true);
        }
        // Desactivar todos los colliders
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders) col.enabled = false;

        if (animator != null) animator.SetTrigger("muerte");

        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 inicio = new Vector3(transform.position.x, alturaMinima, 0);
        Vector3 fin = new Vector3(transform.position.x, alturaMaxima, 0);
        Gizmos.DrawLine(inicio, fin);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccionAtaque);
    }
}