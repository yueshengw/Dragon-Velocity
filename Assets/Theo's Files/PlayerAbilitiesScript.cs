using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public float movementInput;
    public int playerDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        DashMove();
        StompMove();
        //BrakeMove();
        GlideMove();

        if (Input.GetKey(KeyCode.A))
        {
            movementInput = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementInput = 1;
        }


    }

    void DashMove()
    {
        if (playerDirection == 0)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (movementInput < 0)
                {
                    playerDirection = 1;
                }
                else if (movementInput > 0)
                {
                    playerDirection = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                playerDirection = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (playerDirection == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if (playerDirection == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }

    /*void BrakeMove()
    {

    }*/
    void StompMove()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GetComponent<PlayerMovementScript>().isGrounded == false)
            {
                if (playerDirection == 0)
                {
                    rb.velocity = Vector2.down * dashSpeed;
                }
            }
        }
    }
    void GlideMove()
    {
        if (Input.GetKey(KeyCode.K))
        {
            rb.drag = 12;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            rb.drag = 0;
        }
    }
}
