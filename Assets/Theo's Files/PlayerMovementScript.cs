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

    public int jumpNum;
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
                if (GetComponent<Rigidbody2D>().velocity.x < leftMoveForce_Ground.x)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-11f, GetComponent<Rigidbody2D>().velocity.y);
                }
                GetComponent<Rigidbody2D>().AddForce(leftMoveForce_Ground);
            }
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.x < leftMoveForce_Air.x)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-8f, GetComponent<Rigidbody2D>().velocity.y);
                }
                GetComponent<Rigidbody2D>().AddForce(leftMoveForce_Air);
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
        Debug.Log(GetComponent<Rigidbody2D>().velocity.x);

        if (Input.GetKey(KeyCode.D))
        {
            if (isGrounded)
            {
                if (GetComponent<Rigidbody2D>().velocity.x < rightMoveForce_Ground.x)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(11f, GetComponent<Rigidbody2D>().velocity.y);
                }
                GetComponent<Rigidbody2D>().AddForce(rightMoveForce_Ground);
            }
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.x < rightMoveForce_Air.x)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(8f, GetComponent<Rigidbody2D>().velocity.y);
                }
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && jumpNum > 0)
        {
            jumpNum-=1;
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
        if (collision.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
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
            jumpNum = 2;
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