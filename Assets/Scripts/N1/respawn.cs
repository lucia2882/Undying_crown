using UnityEngine;

public class respawn : MonoBehaviour

{
    private Vector3 respawnPoint;
    public Vector3 checkpoint;
    public GameObject fallDetector;
    public GameObject check;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPoint=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        fallDetector.transform.position=new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag=="fallDetector")
        {
            transform.position=respawnPoint;
        }
        if (collision.tag=="check")
        {
            respawnPoint=checkpoint;
        }
    }

}
