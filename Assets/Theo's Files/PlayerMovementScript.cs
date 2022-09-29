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
    //public int playerDirection;
    public int playerHealth;

    private Rigidbody rb;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(leftMoveForce);
            //playerDirection = -1;
            flip = true;
            if (flip == true)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(rightMoveForce);
            //playerDirection = 1;
            flip = false;
            if (flip == false)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(upMoveForce);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            playerHealth -= 1;
        }
    }
}
