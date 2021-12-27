using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public bool canDoubleJump;
    bool doubleJump;
    // Stop unlimited jump
    private bool isGrounded;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    [SerializeField] AudioClip jumpSound;
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
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementSpeed * moveX, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, 0.2f, groundLayer);
        if (isGrounded) doubleJump = true;
        JumpController();
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


}
