using UnityEngine;

// Este script engaña a la bala haciéndose pasar por un "Enemigos" común
public class BossDamageReceiver : Enemigos 
{
    private FinalBoss bossPrincipal;

    void Start() {
        bossPrincipal = GetComponent<FinalBoss>();
    }

    // Sobrescribimos la función que busca la bala
    public override void TakeDamage(int damage) 
    {
        if (bossPrincipal != null) {
            bossPrincipal.TakeDamage(damage);
        }
    }

    // Para que la bala no dé error al buscar IsDead
    public override bool IsDead() {
        // Aquí puedes conectar con una variable del boss si la tienes
        return false; 
    }
}