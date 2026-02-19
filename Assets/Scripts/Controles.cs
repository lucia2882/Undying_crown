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
                SpriteRenderer.flipX = true;   // Mira a la izquierda
            }
        else if (Horizontal > 0)
            {
                SpriteRenderer.flipX = false;  // Mira a la derecha
            }

    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal, Rigidbody2D.linearVelocity.y);
    }
}