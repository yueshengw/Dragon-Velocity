using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    public float multiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            collision.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Downward")
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * multiplier);
            //transform.position = new Vector2(transform.position.x,transform.position.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Downward")
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * multiplier);
        }

    }
}
