using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float movementSpeed;
    public float jumpForce;
    public bool canDoubleJump;
    bool doubleJump;
    // Stop unlimited jump
    private bool isGrounded;
    public bool knocked = false;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [SerializeField] AudioClip jumpSound;

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
        PlayerMovement();
        Animate();
    }

    void PlayerMovement()
    {
        if (knocked == false)
        {
            float moveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(movementSpeed * moveX, rb.velocity.y);
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, 0.2f, groundLayer);
            if (isGrounded) doubleJump = true;
            JumpController();
        }

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
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    PlayerSoundController.instance.PlaySound(jumpSound);
                }
            }
        }
        // if (rb.velocity.y < 5 || rb.velocity.y > 0 && !Input.GetButton("Jump"))
        //     rb.velocity += 7 * Physics2D.gravity.y * Vector2.up * Time.deltaTime;
    }

    void Animate()
    {
        if (rb.velocity.x > 0.0f)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Jump", rb.velocity.y);
    }

    public IEnumerator Knockback(Transform other)
    {
        if (GetComponent<PlayerHealthController>().invincibleState == false)
        {
            knocked = true;
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
            knocked = false;
        }
    }

}
