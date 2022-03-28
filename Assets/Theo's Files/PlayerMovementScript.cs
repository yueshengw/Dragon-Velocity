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
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundCheckRadius;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource jumpingAudio;
    [SerializeField] private AudioSource landAudio;

    public bool isDashing;
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

    void Awake()
    {
        GameManager = GameObject.Find("GameManager");
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        deathTime = deathTimeDefault;
    }
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        GameManager = GameObject.Find("GameManager");
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        GameManager = GameObject.Find("GameManager");

        inputX = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(inputX * speed, rb2D.velocity.y);

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded == true)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            jumpingAudio.Play();
        }

        if (Input.GetKey(KeyCode.A))
        {
            flip = true;
            if (flip == true)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            flip = false;
            if (flip == false)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
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

        if (collider.gameObject.tag == "Ground")
        {
            isGrounded = true;
            canDash = true;
            landAudio.Play();
        }

        //All Scene Transitions
        if (collider.gameObject.tag == "Outskirts Door")
        {
            SceneManager.LoadScene("TheosScene");
        }
        if (collider.gameObject.tag == "Forest Door")
        {
            //play dragon mother cutscene
        }
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
        }
        if (collider.gameObject.tag == "Ground")
        {
            isGrounded = true;
            canDash = true;
        }
        if(collider.gameObject.tag == "Death")
        {
            isDead = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}