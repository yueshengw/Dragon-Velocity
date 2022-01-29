using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float moveSpeed;
    public float jumpSpeed;
    public float moveInput;
    
    public int jumpNumber;
    public Vector3 moveForce;

    //float maxSpeed = 10f;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        //rb2d.AddForce(0, moveInput * moveSpeed, rb2d.velocity.y);
        //rb2d.velocity = new Vector3(moveInput * moveSpeed, rb2d.velocity.y, 0);
        moveForce = new Vector3(moveInput * moveSpeed, rb2d.velocity.y, 0);
        if (moveInput != 0) {
            //rb2d.AddForce(moveForce);
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
        else {
            //rb2d.velocity = Vector3.ClampMagnitude(rb2d.velocity, maxSpeed); 
        }   
        if (Input.GetKey(KeyCode.Space))
        {
            rb2d.velocity = Vector2.up * jumpSpeed;
        }
    }
}
