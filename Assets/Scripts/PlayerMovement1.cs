using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement1 : MonoBehaviour
{
    public GameObject GameManager;
    public float moveSpeed;
    public float moveSpeed_copy;
    public float jump;
    public float moveVelocity;

    public bool grounded;

    public bool isDead;

    public Rigidbody2D rb2d;

    public float moveSpeed1;

    public float moveInput;
    
    public int jumpNumber;
    public Vector3 moveForce;

    public GameObject Player;

    public bool isDashing;
    public bool canDash;
    public float dashValue;
    public float divideValue;
    public int divideIntValue;

    public float glideTimeDefault;
    public float glideTime;

    public float extraJumpTime;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    public float dashTimeDefault;
    public float dashTime;

    public float dashSpeed;
    //private float dashTime;
    public float startDashTime;
    public float movementInput;
    public int playerDirection;


    //public bool isDashing;

    public float onGroundMovingTime;
    public float movingTime;
    public float leftAcceleration;
    public float rightAcceleration;
    public bool newCheckpoint;

    public float dashCoolDown;

    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource jumpingAudio;
    [SerializeField] private AudioSource landAudio;
    [SerializeField] private AudioSource dashAudio;
    [SerializeField] private AudioSource groundpoundAudio;
    [SerializeField] private AudioSource glideAudio;

    void Awake() {
        grounded = false;
        GameManager = GameObject.Find("GameManager");
        rb2d = GetComponent<Rigidbody2D>();
        moveSpeed_copy = moveSpeed;
        glideTime = glideTimeDefault;
        dashTime = dashTimeDefault;
    }
    void Update () 
    {
        moveVelocity = 0;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
        {
            GetComponent<SpriteRenderer>().flipX = true;
            /**
            if (grounded == true)
            {
                onGroundMovingTime += Time.fixedDeltaTime;

                if (onGroundMovingTime > 0.05f)
                {
                    if (rightAcceleration < 19f)
                    {
                        rightAcceleration += 1f;
                    }
                }
                else
                {
                    rightAcceleration = 0f;
                }
            }
            else
            {
                onGroundMovingTime = 0f;
                rightAcceleration = 0f;
            }
            */
            movingTime += Time.fixedDeltaTime;
            if (movingTime > 0.1f)
            {
                if (rightAcceleration < 2f)
                {
                    rightAcceleration += 0.7f;
                }
            }
            else
            {
                rightAcceleration = 0f;
            }
            moveVelocity = - moveSpeed- rightAcceleration;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) 
        {
            GetComponent<SpriteRenderer>().flipX = false;
            /**
            if (grounded == true)
            {
                onGroundMovingTime += Time.fixedDeltaTime;

                if (onGroundMovingTime > 0.05f)
                {
                    if (leftAcceleration < 19f)
                    {
                        leftAcceleration += 1f;
                    }
                }
                else
                {
                    leftAcceleration = 0f;
                }
            }
            else
            {
                onGroundMovingTime = 0f;
                leftAcceleration = 0f;
            }
            **/
            movingTime += Time.fixedDeltaTime;
            if (movingTime > 0.1f)
            {
                if (leftAcceleration < 2f)
                {
                    leftAcceleration += 0.7f;
                }
            }
            else
            {
                leftAcceleration = 0f;
            }
            moveVelocity = moveSpeed + leftAcceleration;
        }
        
        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            movingTime = 0f;
            rightAcceleration = 0f;
            leftAcceleration = 0f;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (grounded == false)
            {
                if (playerDirection == 0)
                {
                    groundpoundAudio.Play();
                    rb2d.velocity = Vector2.down * dashSpeed;
                }
            }
        }
        /*
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.down * 10.0f;
            rb2d.MovePosition(transform.position + (Vector3.down * 2.0f));
        }
        */
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded) 
        {
            jumpingAudio.Play();
            rb2d.velocity = new Vector2(rb2d.velocity.x, jump);
            //rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0 && !(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.K) && !grounded && !Input.GetKey(KeyCode.Space) && rb2d.velocity.y <= 0f && glideTime > 0f) 
        {
            glideTime -= Time.deltaTime;
            rb2d.gravityScale = 1.0f;
            moveSpeed = moveSpeed_copy * 1.3f;
        }
        else
        {
            rb2d.gravityScale = 5.0f;
            moveSpeed = moveSpeed_copy;
        }

        if (Input.GetKey(KeyCode.Q)) 
        {
            transform.position = new Vector3(-28.8f, 101.7f, 0f);
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        moveForce = new Vector3(moveInput * moveSpeed1, rb2d.velocity.y, 0);
        //Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("hi");
        }
        if (Input.GetKeyDown(KeyCode.H) && canDash == true)
        {   
            //Debug.Log("H Pressed");
            //rb2d.gravityScale = 1.0f;
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) {
                //rb2d.MovePosition(new Vector2(transform.position.x + (1 * 15.0f), transform.position.y + (1 * 15.0f)));
                //rb2d.transform.Translate(new Vector3(dashValue * 0.8f, dashValue * 0.8f, 0f));
                for (int i = divideIntValue; i >= 0; i--)
                {
                    rb2d.MovePosition(new Vector2(transform.position.x + dashValue/ divideValue, transform.position.y + dashValue/ divideValue));
                }
                //Debug.Log("hi");
                //rb2d.velocity = Vector2.right * 500f;
                dashAudio.Play();
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) {
                //rb2d.MovePosition(new Vector2(transform.position.x + (Vector3.right.x * 15.0f), transform.position.y + (Vector3.up.y * 15.0f)));
                //rb2d.transform.Translate(new Vector3(-dashValue*0.8f, dashValue * 0.8f, 0f));
                for (int i = divideIntValue; i >= 0; i--)
                {
                    rb2d.MovePosition(new Vector2(transform.position.x - dashValue/ divideValue, transform.position.y + dashValue/ divideValue));
                }
                //rb2d.velocity = Vector2.right * 500f;
                dashAudio.Play();
            }
            else if (moveInput == 1)
            {
                //rb2d.MovePosition(transform.position + (Vector3.right * 15.0f));
                for (int i = divideIntValue; i >= 0; i--)
                {
                    rb2d.MovePosition(new Vector2(transform.position.x + dashValue/ divideValue, transform.position.y));
                }
                //rb2d.transform.Translate(new Vector3(dashValue, 0f, 0f));
                //rb2d.velocity = Vector2.left * 500f;
                dashAudio.Play();
            }
            else if (moveInput == -1)
            {
                //rb2d.MovePosition(transform.position + (Vector3.left * 15.0f));
                rb2d.MovePosition(new Vector2(transform.position.x - dashValue, 0f));
                for (int i = divideIntValue; i >= 0; i--)
                {
                    rb2d.MovePosition(new Vector2(transform.position.x - dashValue/ divideValue, transform.position.y));
                }
                //rb2d.transform.Translate(new Vector3(-dashValue, 0f, 0f));
                //rb2d.velocity = Vector2.left * 500f;
                dashAudio.Play();
            }
            //rb2d.gravityScale = 5.0f;
            dashCoolDown = 0.5f;
            isDashing = true;
            canDash = false;
            dashTime = dashTimeDefault;
        }
        //DashMove();
        if (dashTime > 0)
        {
            dashTime -= Time.fixedDeltaTime;
        }
        else
        {
            isDashing = false;
        }
        if (isDashing == false)
        {
            dashCoolDown -= Time.fixedDeltaTime;
        }
        rb2d.velocity = new Vector2 (moveVelocity, rb2d.velocity.y);
        if (Input.GetKey(KeyCode.R))
        {
            isDead = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && grounded == true)
        {
            if (!walkingAudio.isPlaying)
            {
                walkingAudio.Play();
            }
        }
        else
        {
            walkingAudio.Stop();
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (!glideAudio.isPlaying)
            {
                glideAudio.Play();
            }
        }
        else
        {
            glideAudio.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Death")
        {
            isDead = true;
        }
        if (collider.tag == "Ground")
        {
            glideTime = glideTimeDefault;
            grounded = true;
            canDash = true;
            landAudio.Play();
        }
        if (collider.tag == "Trigger1")
        {
            GameManager.GetComponent<GameManager>().Call1();
            collider.gameObject.SetActive(false);
        }
        if (collider.tag == "Outskirts Door")
        {
            SceneManager.LoadScene("TheosScene");
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Death")
        {
            isDead = true;
        }
        if (collider.tag == "Ground")
        {
            grounded = true;
            canDash = true;
        }
    }
    private void OnTriggerExit2D()
    {
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Death")
        {
            isDead = true;
        }
        if (collider.gameObject.tag == "Breakable" && isDashing == true)
        {
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Ground")
        {
            canDash = true;
        }
    }
    void DashMove()
    {
        if (moveInput == 0)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                //dashAudio.Play();
                if (movementInput == -1)
                {
                    playerDirection = 1;
                }
                else if (movementInput == 1)
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
                rb2d.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (playerDirection == 1)
                {
                    rb2d.velocity = Vector2.left * dashSpeed;
                    isDashing = true;
                }
                else if (playerDirection == 2)
                {
                    rb2d.velocity = Vector2.right * dashSpeed;
                    isDashing = true;
                }
            }
        }
    }
}