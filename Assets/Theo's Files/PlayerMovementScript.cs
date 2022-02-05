using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 leftMoveForce_Ground;
    public Vector3 rightMoveForce_Ground;
    public Vector3 leftMoveForce_Air;
    public Vector3 rightMoveForce_Air;
    public Vector3 upMoveForce;

    public bool flip;
    public bool isGrounded;
    public bool isDead;
    public bool isMoving;

    //public int playerDirection;
    public int playerHealth;

    private Rigidbody rb;

    [SerializeField] private AudioSource walkingAudio;
    [SerializeField] private AudioSource jumpingAudio;
    [SerializeField] private AudioSource landAudio;

    public bool isDashing;
    public bool canDash;
    public float jump;
    public GameObject GameManager;
    public bool newCheckpoint;

    public float maxSpeed;
    void Start()
    {
        isDead = false;
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, maxSpeed);
        if (Input.GetKey(KeyCode.A))
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(leftMoveForce_Ground.x*2,0,0));
            }
            else
            {
                //GetComponent<Rigidbody2D>().AddForce(leftMoveForce_Air.x*2,0,0);
            }
            //GetComponent<Rigidbody2D>().AddForce(leftMoveForce);
            //GetComponent<Rigidbody2D>().AddForce(leftMoveForceCounter);
            flip = true;
            isMoving = true;
            if (flip == true)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(rightMoveForce_Ground);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(rightMoveForce_Air);
            }
            //GetComponent<Rigidbody2D>().AddForce(rightMoveForceCounter);
            flip = false;
            isMoving = true;
            if (flip == false)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        /*if (isMoving == true)
        {
            walkingAudio.Play();
        }
        else
        {
            walkingAudio.Stop();
        }*/
        /**
        if (isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            jumpingAudio.Play();
            GetComponent<Rigidbody2D>().AddForce(upMoveForce);
            isGrounded = false;
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            jumpingAudio.Play();
            GetComponent<Rigidbody2D>().AddForce(upMoveForce);
            isGrounded = false;
        }
        **/
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && isGrounded)
        {
            jumpingAudio.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (isDead == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            landAudio.Play();
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
        }
        if (collision.gameObject.tag == "Death")
        {
            isDead = true;
        }
        if (collision.gameObject.tag == "Breakable" && isDashing == true)
        {
            Destroy(GetComponent<Collider>().gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
        }
        if (collision.gameObject.tag == "Death")
        {
            isDead = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
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
            //glideTime = glideTimeDefault;
            isGrounded = true;
            //canDash = true;
        }
        if (collider.tag == "Trigger1")
        {
            GameManager.GetComponent<GameManager>().Call1();
            collider.gameObject.SetActive(false);
        }
        if (collider.tag == "Ground")
        {
            isGrounded = true;
            //canDash = true;
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
            isGrounded = true;
            //canDash = true;
        }
    }
    private void OnTriggerExit2D()
    {
        isGrounded = false;
    }
}