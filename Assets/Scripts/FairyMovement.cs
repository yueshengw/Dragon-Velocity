using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMovement : MonoBehaviour
{
    public TrailRenderer trail;
    public float timer;
    public bool playerClose;
    public bool canEmit;
    public float actionTime;
    private float timer2;
    public GameObject fairyMovement;
    public float timer3;
    public float increment;
    public float maxSpeed;
    private float defaultSpeed;
    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = fairyMovement.GetComponent<FollowThePath>().moveSpeed;
        timer2 = actionTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            //trail.emitting = true;
            canEmit = true;
        }

        if (canEmit && timer2 > 0f)
        {
            timer2 -= Time.deltaTime;
            trail.emitting = true;
            timer3 += Time.deltaTime;
            if (fairyMovement.GetComponent<FollowThePath>().moveSpeed < maxSpeed)
            {
                fairyMovement.GetComponent<FollowThePath>().moveSpeed += increment * timer3;
                //Debug.Log(fairyMovement.GetComponent<FollowThePath>().moveSpeed);
            }
            
        }
        else if (canEmit == false)
        {
            trail.emitting = false;
            timer2 = actionTime;
            timer3 = 1f;
            fairyMovement.GetComponent<FollowThePath>().moveSpeed = defaultSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerClose = true;
            timer2 = actionTime;
            canEmit = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerClose = true;
            timer2 = actionTime;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canEmit = false;
        }
    }
}
