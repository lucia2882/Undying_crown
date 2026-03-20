using UnityEngine;

public class Salto : MonoBehaviour

{
    public float Speed = 5f;
    public float JumpForce = 5f;
    public LayerMask GroundLayer;

    private Rigidbody2D rb;
    private float horizontal;
    private bool grounded;
    public Animator salto;

    [Header("Configuración Doble Salto")]
    public bool puedeDobleSalto = false;
    private bool yaHizoDobleSalto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 4.5f, GroundLayer);
        grounded = hit.collider != null;

        if (grounded)
        {
            yaHizoDobleSalto = false;
        }

        Debug.DrawRay(transform.position, Vector2.down * 4.5f, Color.red);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                EjecutarFuerzaSalto();
            }
            else if (puedeDobleSalto && !yaHizoDobleSalto)
            {
                EjecutarFuerzaSalto();
                yaHizoDobleSalto = true; 
            }
        }
    }

    private void EjecutarFuerzaSalto()
    {

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        
        if (salto != null) 
            salto.SetTrigger("salto");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * Speed, rb.linearVelocity.y);
    }
}