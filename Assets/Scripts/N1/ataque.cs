using UnityEngine;
using System.Collections;

public class ataque : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f; 
    public LayerMask enemyLayers; 
    public int damage = 20;
    public Animator animacion;
    public float cooldown;
    private bool puedoatacar;

    void Start()
    {
        puedoatacar = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && puedoatacar==true)
        {
            animacion.SetTrigger("ataque");
            StartCoroutine(cool());
        }
    }
    

    void Attack()
    {
        // Detectar enemigos en el rango del ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        // Dañar a cada enemigo detectado
        foreach(Collider2D enemy in hitEnemies)
        {
            // Buscamos el script de Salud en el enemigo y llamamos a su función
            enemy.GetComponent<Enemigos>().TakeDamage(damage);
        }
    }
    IEnumerator cool()
    {
        puedoatacar = false;
        Attack();
        yield return new WaitForSeconds(cooldown);
        {
            puedoatacar = true;
        }
    }
    // Esto sirve para ver el círculo de ataque en el editor de Unity (no se ve en el juego)
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}