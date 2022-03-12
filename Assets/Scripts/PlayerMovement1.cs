using System.Collections;
using System.Collections.Generic;
using SpriteGlow;
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
    public bool isGroundPounding;
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
    public float startDashTime;
    public float movementInput;
    public int playerDirection;

    public float onGroundMovingTime;
    public float movingTime;
    public float leftAcceleration;
    public float rightAcceleration;
    public bool newCheckpoint;

    public float dashCoolDown;
    /**
    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed1 = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing1;

    [Space]

    private bool groundTouch;
    private bool hasDashed;
    **/
    public int side = 1;
    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource jumpingAudio;
    [SerializeField] private AudioSource landAudio;
    [SerializeField] private AudioSource dashAudio;
    [SerializeField] private AudioSource groundpoundAudio;
    [SerializeField] private AudioSource glideAudio;

    public Material material;

    public bool isDissolving = false;
    public float fade = 1f;
    public float deathTimeDefault;
    public float deathTime;
    public bool respawn;

    public bool inputDisabled;

    public GameObject DashGlow;
    //public SpriteGlowEffect spriteGlowEffect; 
    void Awake() {
        grounded = false;
        GameManager = GameObject.Find("GameManager");
        rb2d = GetComponent<Rigidbody2D>();
        moveSpeed_copy = moveSpeed;
        glideTime = glideTimeDefault;
        dashTime = dashTimeDefault;
        //material = GetComponent<SpriteRenderer>().material;
        deathTime = deathTimeDefault;
        //spriteGlowEffect = GetComponent<SpriteGlowEffect>();
    }
    void Update () 
    {
        moveVelocity = 0;
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDissolving = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DashGlow.SetActive(false);
        }
        if (isDissolving == true || isDead == true)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
            }
            material.SetFloat("_DissolveAmount", fade);
        }
        if (respawn == true)
        {
            if (deathTime > 0f)
            {
                deathTime -= Time.deltaTime;
            }
            else if (deathTime <= 0f)
            {
                isDead = false;
            }
        }
        if (isDead == false)
        {
            fade = 1f;
            material.SetFloat("_DissolveAmount", fade);
            respawn = false;
            deathTime = deathTimeDefault;
            inputDisabled = false;
        }

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

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && inputDisabled == false) 
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
        
        if ((!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))) && inputDisabled == false)
        {
            movingTime = 0f;
            rightAcceleration = 0f;
            leftAcceleration = 0f;
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && inputDisabled == false)
        {
            if (grounded == false)
            {
                if (playerDirection == 0)
                {
                    groundpoundAudio.Play();
                    rb2d.velocity = Vector2.down * dashSpeed;
                    isGroundPounding = true;
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded && inputDisabled == false) 
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
            rb2d.drag = 5.0f;
            moveSpeed = moveSpeed_copy * 1.3f;
        }
        else
        {
            rb2d.drag = 0f;
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
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.H) && canDash == true)
        {
            if (xRaw != 0 || yRaw != 0) { }
                //Dash(xRaw, yRaw);
        }
        //DashGlow.SetActive(true);

        if (Input.GetKeyDown(KeyCode.H) && canDash == true && inputDisabled == false)
        {
            //GetComponent<SpriteGlowEffect>().out
            //Debug.Log("H Pressed");
            //rb2d.gravityScale = 1.0f;
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) {
                DashGlow.SetActive(true);
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
                DashGlow.SetActive(true);
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
                DashGlow.SetActive(true);
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
                DashGlow.SetActive(true);
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
            DashGlow.SetActive(false);
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
            //DashGlow.SetActive(true);
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
            DashGlow.SetActive(false);
            //isDead = true;
            fade = 0.85f;
            material.SetFloat("_DissolveAmount", fade);
            inputDisabled = true;
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

        //All Scene Transitions
        if (collider.tag == "Outskirts Door")
        {
            SceneManager.LoadScene("TheosScene");
        }
        if (collider.tag == "Forest Door")
        {
            //play dragon mother cutscene
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
            DashGlow.SetActive(false);
            isDead = true;
            inputDisabled = true;
        }
        if (collider.gameObject.tag == "Breakable" && isDashing == true)
        {
            Destroy(collider.gameObject);
        }
        if(collider.gameObject.tag == "Breakable" && isGroundPounding == true)
        {
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Ground")
        {
            canDash = true;
            isGroundPounding = false;
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
    /**
    private void Dash(float x, float y)
    {
        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        //FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;

        //anim.SetTrigger("dash");

        rb2d.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb2d.velocity += dir.normalized * dashSpeed1;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        //FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        //DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        //dashParticle.Play();
        rb2d.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing1 = true;

        yield return new WaitForSeconds(.3f);

        //dashParticle.Stop();
        rb2d.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        //wallJumped = false;
        isDashing1 = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        //if (coll.onGround)
        //    hasDashed = false;
    }
    */
}