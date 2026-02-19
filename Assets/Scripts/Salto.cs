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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 4.5f, GroundLayer);
        grounded = hit.collider != null;

        Debug.DrawRay(transform.position, Vector2.down * 4.5f, Color.red);

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
            salto.SetTrigger ("salto");
        }
        
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * Speed, rb.linearVelocity.y);
    }
}