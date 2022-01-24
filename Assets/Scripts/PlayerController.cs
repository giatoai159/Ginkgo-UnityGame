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
    bool lockPlayer;
    public bool lockPlayerMovement { get { return lockPlayer; } set { lockPlayer = value; } }
    public float coyoteTime;
    float coyoteTimer;
    public float jumpBufferTime;
    float jumpBufferTimer;
    [Header("Grounding")]
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [Header("Sound")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip dashSound;
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
        if (transform.position.y < -30) rb.velocity = new Vector2(0f, 0f);
        if (PlayerHealthController.instance.invincibleState == false && canMove == false) canMove = true;
        PlayerInteract();
    }

    void PlayerMovement()
    {
        if (canMove && !lockPlayer)
        {
            float moveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(movementSpeed * moveX, rb.velocity.y);
            JumpController();
            Dash();
        }
    }

    void PlayerInteract()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, faceLeft ? Vector2.left : Vector2.right, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("NPC"))
                {
                    NPC npc = hit.collider.GetComponent<NPC>();
                    npc.DisplayDialog();
                }
                if (hit.collider.CompareTag("Tree"))
                {
                    Tree tree = hit.collider.GetComponent<Tree>();
                    tree.ReturnElement();
                }
            }
        }
    }

    void JumpController()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, 0.2f, groundLayer);
        if (isGrounded)
        {
            if (canDoubleJump)
                doubleJump = true;
            coyoteTimer = coyoteTime;
        }
        else coyoteTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
            jumpBufferTimer = jumpBufferTime;
        else if (jumpBufferTimer >= 0f) jumpBufferTimer -= Time.deltaTime;

        if (canDoubleJump)
        {
            if (coyoteTimer > 0f && jumpBufferTimer > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                PlayerSoundController.instance.PlaySound(jumpSound);
                jumpBufferTimer = 0;
            }

            else
            {
                if (doubleJump && Input.GetButtonDown("Jump"))
                {
                    Instantiate(dashEffect, transform.position, Quaternion.identity);
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    PlayerSoundController.instance.PlaySound(jumpSound);
                    doubleJump = false;
                }
            }
        }
        else
        {
            if (coyoteTimer > 0f && jumpBufferTimer > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                PlayerSoundController.instance.PlaySound(jumpSound);
                jumpBufferTimer = 0;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
            coyoteTimer = 0;
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
        if (PlayerHealthController.instance.invincibleState == false)
        {
            canMove = false;
            for (float i = 0; i <= 0.3f; i += 0.1f)
            {
                Vector2 knockBackDirection = (other.transform.position - this.transform.position).normalized;
                rb.AddForce(new Vector2(150, 150) * -knockBackDirection);
                yield return new WaitForSeconds(0.1f);
            }
            canMove = true;
        }
        canMove = true;
    }

}
