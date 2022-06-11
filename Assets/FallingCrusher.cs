using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCrusher : MonoBehaviour
{
    public float coolDownTime;
    private float countTimer;
    private bool touchGround;
    private bool touchUpper;

    public float fallGravity = 30f;
    public float upGravity = -4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchGround == true)
        {
            countTimer += Time.deltaTime;
        }
        else
        {
            countTimer = 0f;
        }

        if (countTimer >= coolDownTime & touchGround == true)
        {
            GetComponent<Rigidbody2D>().gravityScale = upGravity;
            touchGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" & touchUpper)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().gravityScale = fallGravity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            touchGround = true;
        }
        if (collision.gameObject.tag == "Upper")
        {
            touchUpper = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Upper")
        {
            touchUpper = false;
        }
    }
}
