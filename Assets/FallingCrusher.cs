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

    private GameObject player;
    public bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
        GetComponent<Rigidbody2D>().gravityScale = upGravity;
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

        if (isTriggered & touchUpper)
        {
            gameObject.tag = "Death";
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().gravityScale = fallGravity;
            //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            touchGround = true;
            gameObject.tag = "Untagged";
            gameObject.layer = 13;
        }
        if (collision.gameObject.tag == "Upper")
        {
            touchUpper = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.layer = 10;
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
