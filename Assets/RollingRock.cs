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
