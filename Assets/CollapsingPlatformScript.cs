using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatformScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 startPosition;

    public bool canRespawn;
    public float respawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRespawn == true)
        {
            respawnTimer += Time.deltaTime;

            if (respawnTimer >= 3)
            {
                rb.isKinematic = true;
                rb.velocity = new Vector3(0, 0, 0);
                canRespawn = false;
                respawnTimer = 0;
                GetComponent<PolygonCollider2D>().isTrigger = false;
                transform.position = startPosition;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            Invoke("DropPlatform", 0.5f);
        }
    }
    void DropPlatform()
    {
        rb.isKinematic = false;
        GetComponent<PolygonCollider2D>().isTrigger = true;
        canRespawn = true;
    }
}
