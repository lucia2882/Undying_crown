using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private Animator Animator;
    public Animator salto;
    private SpriteRenderer SpriteRenderer;
    public float speed = 1f;


    void Start()
    {   
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

    }   

    void Update()
    {
        Horizontal = Input.GetAxisRaw ("Horizontal");
        Animator.SetBool ("andar", Horizontal !=0.0f);
        if (Horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);   // Mira a la izquierda
            }
        else if (Horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);  // Mira a la derecha
            }

    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * speed, Rigidbody2D.linearVelocity.y);
    }
}