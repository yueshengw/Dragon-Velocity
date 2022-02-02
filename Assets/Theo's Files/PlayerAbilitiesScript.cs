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

    [SerializeField] private AudioSource dashAudio;
    [SerializeField] private AudioSource groundpoundAudio;

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
        GroundPoundMove();
        BrakeMove();
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
                dashAudio.Play();
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

    void BrakeMove()
    {
        if (Input.GetKeyDown(KeyCode.J) /*&& GetComponent<PlayerMovementScript>().isGrounded == true*/)
        {
            if (playerDirection == 0)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    void GroundPoundMove()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GetComponent<PlayerMovementScript>().isGrounded == false)
            {
                if (playerDirection == 0)
                {
                    groundpoundAudio.Play();
                    rb.velocity = Vector2.down * dashSpeed;
                }
            }
        }
    }
    void GlideMove()
    {
        if (Input.GetKey(KeyCode.K) && GetComponent<PlayerMovementScript>().isGrounded == false)
        {
            rb.drag = 7;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            rb.drag = 0;
        }
    }
}
