using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 leftMoveForce;
    public Vector3 rightMoveForce;
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

    void Start()
    {
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(leftMoveForce);
            flip = true;
            isMoving = true;
            if (flip == true)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(rightMoveForce);
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
}
