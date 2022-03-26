using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float dashTime;
    public float startDashTime;
    public float movementInput;
    public int playerDirection;

    private bool isGroundPounding;

    [SerializeField] private AudioSource dashAudio;
    [SerializeField] private AudioSource groundpoundAudio;
    [SerializeField] private AudioSource glideAudio;

    [Header("Dashing")]
    [SerializeField] private float dashingVelocity;
    [SerializeField] private float dashingTime;
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundPoundMove();
        BrakeMove();
        GlideMove();
        DashMove();
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
    }

    void DashMove()
    {
        if ((Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.LeftShift)) && canDash)
        {
            isDashing = true;
            canDash = false;
            tr.emitting = true;
            dashAudio.Play();
            dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashingDirection == Vector2.zero)
            {
                dashingDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
            //animator.SetBool("IsDashing", isDashing);
        }

        if (isDashing)
        {
            rb2D.velocity = dashingDirection.normalized * dashingVelocity;
            return;
        }

        if (GetComponent<PlayerMovementScript>().isGrounded == true)
        {
            canDash = true;
        }
    }
    void BrakeMove()
    {
        if (Input.GetKeyDown(KeyCode.J) /*&& GetComponent<PlayerMovementScript>().isGrounded == true*/)
        {
            if (playerDirection == 0)
            {
                rb2D.velocity = Vector2.zero;
            }
        }
    }
    void GroundPoundMove()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GetComponent<PlayerMovementScript>().isGrounded == false)
            {
                if (playerDirection == 0)
                {
                    groundpoundAudio.Play();
                    rb2D.velocity = Vector2.down * dashingVelocity;
                    isGroundPounding = true;
                }
            }
        }
    }
 
    void GlideMove()
    {
        if (Input.GetKey(KeyCode.K) && GetComponent<PlayerMovementScript>().isGrounded == false)
        {
            rb2D.drag = 10;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            rb2D.drag = 0;
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

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Breakable" && isDashing == true)
        {
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Breakable" && isGroundPounding == true)
        {
            Destroy(collider.gameObject);
        }
    }
}
