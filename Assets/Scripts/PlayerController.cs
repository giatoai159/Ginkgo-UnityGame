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
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
                    PlaySound(audioClip);
                }

                else
                {
                    if (doubleJump)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.7f);
                        PlaySound(audioClip);
                        doubleJump = false;
                    }
                }
            }
            else
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    PlaySound(audioClip);
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

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
