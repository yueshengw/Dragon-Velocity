using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public bool flip;
    public bool isGrounded;
    public bool isDead;

    //public int playerDirection;
    public int playerHealth;

    private Rigidbody2D rb2D;
    private Animator anim;
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundCheckRadius;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource jumpingAudio;
    [SerializeField] private AudioSource landAudio;

    public bool isDashing;
    public bool isMoving;
    public bool canDash;
    public GameObject GameManager;
    public bool newCheckpoint;

    public Material material;

    public bool isDissolving = false;
    public float fade = 1f;
    public float deathTimeDefault;
    public float deathTime;
    private float inputX;
    public bool respawn;

    public bool inputDisabled;
    public TrailRenderer tr;
    public bool debug;
    public TrailRenderer debugTrail;

    void Awake()
    {
        GameManager = GameObject.Find("GameManager");
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        deathTime = deathTimeDefault;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Start()
    {
        isDead = false;
        GameManager = GameObject.Find("GameManager");
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        GameManager = GameObject.Find("GameManager");
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        inputX = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(inputX * speed, rb2D.velocity.y);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded == true)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            jumpingAudio.Play();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            flip = true;

            if (flip == true)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            flip = false;

            if (flip == false)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving == true)
        {
            anim.SetBool("IsRunning", true);
        }
        else if (isMoving == false)
        {
            anim.SetBool("IsRunning", false);
        }

        if (isGrounded == false)
        {
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && isGrounded == true)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
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
        if (debug == true)
        {
            //debugTrail.emitting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
        }

        if (collider.gameObject.tag == "Death")
        {
            fade = 0.85f;
            material.SetFloat("_DissolveAmount", fade);
            inputDisabled = true;
        }

        if ((collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Slippery" || collider.gameObject.tag == "Breakable_Floor"))
        {
            isGrounded = true;
            canDash = true;
            anim.SetBool("IsJumping", false);
            landAudio.Play();
        }

        //All Scene Transitions
        if (collider.gameObject.tag == "Outskirts Door")
        {
            SceneManager.LoadScene("The Outskirts");
        }

        if (collider.gameObject.tag == "Forest Door")
        {
            //play dragon mother cutscene
            SceneManager.LoadScene("MotherDragonMeeting");
        }

        if (collider.gameObject.tag == "Slippery")
        {
            speed = 60;
            jumpForce = 65;
            tr.emitting = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
        }

        if ((collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Breakable_Floor" || collider.gameObject.tag == "Slippery"))
        {
            isGrounded = true;
            canDash = true;
            anim.SetBool("IsJumping", false);
        }

        if (collider.gameObject.tag == "Death")
        {
            isDead = true;
        }
        if (collider.gameObject.tag == "Slippery")
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 10, ForceMode2D.Impulse);
        }
        if (collider.gameObject.tag == "Slippery")
        {
            speed = 60;
            jumpForce = 65;
            //tr.emitting = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Slippery" || collider.gameObject.tag == "Breakable_Floor")
        {
            isGrounded = false;
            anim.SetBool("IsJumping", true);
        }

        if (collider.gameObject.tag == "Slippery")
        {
            StartCoroutine(CoroutineSlip());
        }
        if (collider.gameObject.tag == "Breakable_Floor")
        {
            isGrounded = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fly")
        {
            speed = 100;
            //jumpForce = 30;
        }
        if (collision.gameObject.tag == "Death")
        {
            fade = 0.85f;
            material.SetFloat("_DissolveAmount", fade);
            inputDisabled = true;
            isDead = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fly")
        {
            speed = 35;
            jumpForce = 30;
        }
    }
    IEnumerator CoroutineSlip()
    {
        yield return new WaitForSeconds(1.5f);
        speed = 35;
        jumpForce = 30;
        tr.emitting = false;
    }
}