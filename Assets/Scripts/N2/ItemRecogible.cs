using UnityEngine;

public class ItemRecogible : MonoBehaviour
{
    public enum TipoItem { Mochila, Corazon, Reloj, TP }
    public TipoItem tipo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventarioJugador inv = collision.GetComponent<InventarioJugador>();
            
            // 1. La Mochila: Se recoge siempre y activa la capacidad extra
            if (tipo == TipoItem.Mochila)
            {
                inv.tieneMochila = true;
                inv.RecogerObjeto(0); // Activa la imagen 0 en el Canvas
                Destroy(gameObject);
                return;
            }

            // 2. Otros ítems: Revisar si hay espacio en el inventario
            if (inv.PuedeRecoger())
            {
                switch (tipo)
                {
                    case TipoItem.Corazon:
                        vida v = collision.GetComponent<vida>();
                        if (v != null)
                        {
                            // CAMBIO AQUÍ: Llamamos a la nueva función que creamos
                            v.AumentarVidaMaxima();
                        }
                        inv.RecogerObjeto(1); // Activa imagen 1
                        break;

                    case TipoItem.Reloj:
                        RelojMuerte reloj = FindFirstObjectByType<RelojMuerte>();
                        if (reloj != null)
                        {
                            reloj.tiempoRestante += 60f; // Añade 1 minuto (60s)
                        }
                        inv.RecogerObjeto(2); // Activa imagen 2
                        break;

                    case TipoItem.TP:
                        inv.RecogerObjeto(3); // Activa imagen 3
                        break;
                }
                
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("¡Inventario lleno! Necesitas la mochila.");
            }
        }
    }
}