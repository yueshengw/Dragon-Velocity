using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator anim;
    private float dashTime;
    public float startDashTime;
    public float movementInput;
    public int playerDirection;

    private bool isStomping;
    private bool canStomp = true;

    [SerializeField] private float stompForce;

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

    public PlayerMovementScript player;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        tr = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        StompMove();
        BrakeMove();
        GlideMove();
        DashMove();
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        isStomping = false;
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
            anim.SetBool("IsDashing", true);

            rb2D.velocity = dashingDirection.normalized * dashingVelocity;
            return;
        }
        else
        {
            anim.SetBool("IsDashing", false);
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
    void StompMove()
    {
        if (Input.GetKeyDown(KeyCode.S) && canStomp == true)
        {
            tr.emitting = true;
            groundpoundAudio.Play();
            rb2D.velocity = new Vector2(rb2D.velocity.x, stompForce);
            isStomping = true;
            canStomp = false;
            StartCoroutine(StopDashing());
        }

        if (isStomping)
        {
            anim.SetBool("IsStomping", true);
        }
        else
        {
            anim.SetBool("IsStomping", false);
        }
    }

    void GlideMove()
    {
        if ((Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.V)) && GetComponent<PlayerMovementScript>().isGrounded == false)
        {
            rb2D.drag = 15;
            tr.emitting = true;
        }
        else if ((Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.V)))
        {
            rb2D.drag = 0;
            tr.emitting = false;
        }

        if ((Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.V)))
        {
            if (!glideAudio.isPlaying)
            {
                glideAudio.Play();
                anim.SetBool("IsGliding", true);
            }
            if (player.isGrounded == true)
            {
                glideAudio.Stop();
                anim.SetBool("IsGliding", false);
            }
        }
        else
        {
            glideAudio.Stop();
            anim.SetBool("IsGliding", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Breakable_Wall" && isDashing == true)
        {
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Breakable_Floor" && isStomping == true)
        {
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.tag == "Ground")
        {
            canDash = true;
            canStomp = true;
        }
        if (collider.gameObject.tag == "Breakable_Floor")
        {
            canDash = true;
            canStomp = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Breakable_Wall" && isDashing == true)
        {
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "Breakable_Floor" && isStomping == true)
        {
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.tag == "Ground")
        {
            canDash = true;
            canStomp = true;
        }
        if (collider.gameObject.tag == "Breakable_Floor")
        {
            canDash = true;
            canStomp = true;
        }
    }
}
