using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Horizontal Movement")]
    public float movementSpeed;
    Vector2 direction;
    bool faceLeft;
    public bool getFacing { get { return faceLeft; } }
    public bool canDash;
    public float dashForce;
    public float dashCooldown;
    [SerializeField] GameObject dashEffect;
    float dashCooldownTimer;
    public float getDashCDTimer { get { return dashCooldownTimer; } }
    [SerializeField] GameObject dashTrailPrefab;
    [Header("Vertical Movement")]
    public float jumpForce;
    public bool canDoubleJump;
    bool doubleJump;
    // Stop unlimited jump
    bool isGrounded;
    bool canMove = true;
    public bool playerMovement { get { return canMove; } set { canMove = value; } }
    [Header("Grounding")]
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [Header("Sound")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip dashSound;
    bool isDead;
    public bool Respawn { get { return isDead; } set { isDead = value; } }
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (direction.x == 1) faceLeft = false;
        else if (direction.x == -1) faceLeft = true;
        if (dashCooldownTimer > 0) dashCooldownTimer -= Time.deltaTime;
        PlayerMovement();
        Animate();
    }

    void PlayerMovement()
    {
        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(movementSpeed * moveX, rb.velocity.y);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, 0.2f, groundLayer);
        if (isGrounded) doubleJump = true;
        JumpController();
        Dash();
    }

    void JumpController()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (canDoubleJump)
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    PlayerSoundController.instance.PlaySound(jumpSound);
                }

                else
                {
                    if (doubleJump)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.7f);
                        PlayerSoundController.instance.PlaySound(jumpSound);
                        doubleJump = false;
                    }
                }
            }
            else
            {
                if (isGrounded)
                {
                    rb.velocity += new Vector2(rb.velocity.x, jumpForce);
                    PlayerSoundController.instance.PlaySound(jumpSound);
                }
            }
        }

    }

    void Dash()
    {
        if (Input.GetButtonDown("Fire3") && canDash && dashCooldownTimer <= 0)
        {
            StartCoroutine(TrailCoroutine());
            StartCoroutine(DashCoroutine());
        }
    }

    void Animate()
    {
        if (!faceLeft)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Jump", rb.velocity.y);
    }

    IEnumerator TrailCoroutine()
    {
        var trail = Instantiate(dashTrailPrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        trail.transform.parent = transform;
        yield return new WaitForSeconds(0.4f);
        Destroy(trail);
    }

    IEnumerator DashCoroutine()
    {
        PlayerSoundController.instance.PlaySound(dashSound);
        PlayerHealthController.instance.invincibleState = true;
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        canMove = false;
        dashCooldownTimer = dashCooldown;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashForce, 0f) * (faceLeft ? -1 : 1), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.17f);
        PlayerHealthController.instance.invincibleState = false;
        canMove = true;
        rb.gravityScale = 5;
    }

    public IEnumerator Knockback(Transform other)
    {
        if (GetComponent<PlayerHealthController>().invincibleState == false)
        {
            canMove = false;
            for (float i = 0; i < 0.3; i += 0.1f)
            {
                Vector2 knockBackDirection = (other.transform.position - this.transform.position).normalized;
                if (knockBackDirection.x < 0) knockBackDirection.x = -1;
                else knockBackDirection.x = 1;
                if (knockBackDirection.y < 0) knockBackDirection.y = -1;
                else knockBackDirection.y = 1;
                rb.AddForce(new Vector2(250, 250) * -knockBackDirection);
                yield return new WaitForSeconds(0.1f);
            }
            canMove = true;
        }
    }

}
