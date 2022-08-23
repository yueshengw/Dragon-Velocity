using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMachine : MonoBehaviour
{
    public float x;
    public float y;
    public float multiply;
    private bool active;
    private float timer;
    public float timeout;
    public float decrease;
    private bool collide;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        decrease = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == false)
        {
            timer += Time.deltaTime;
        }

        if (timer >= timeout)
        {
            active = true;
            timer = 0f;
        }

        if (collide == true)
        {
            decrease += Time.deltaTime;
        }
        else
        {
            decrease = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collide = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (active == true)
            {
                collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * multiply / decrease);
                collider.GetComponent<PlayerMovementScript>().canDash = true;
                //collider.GetComponent<Rigidbody2D>().velocity = new Vector2(x * multiply, y * multiply / decrease);
            }

        }
    }
    private void OnTriggerExit2D()
    {
        //active = false;
        collide = false;
    }
}
