using UnityEngine;

public class Camara : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform player;              
    public string playerTag = "Player";

    [Header("Seguimiento")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.12f;

    [Header("Limites (opcional)")]
    public bool useLimits = false;
    public Vector2 minPos;
    public Vector2 maxPos;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p.transform;
        }
    }

    void LateUpdate()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p.transform;
            else return;
        }

    Vector3 targetPos = new Vector3(player.position.x, player.position.y, 0f) + offset;


        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        if (useLimits)
        {
            newPos.x = Mathf.Clamp(newPos.x, minPos.x, maxPos.x);
            newPos.y = Mathf.Clamp(newPos.y, minPos.y, maxPos.y);
        }

        transform.position = newPos;
    }
}