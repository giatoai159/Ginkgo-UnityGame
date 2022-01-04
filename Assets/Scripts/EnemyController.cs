using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    public float movementSpeed;
    public int damage;
    public float invincibleLength;
    public float invincibleDeltaTime;
    bool isInvincible = false;
    public bool isKnockbackable;
    public Image maskImage;
    float originalSize;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deadSound;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector3 scale;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        originalSize = maskImage.rectTransform.rect.width;
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            PlayerHealthController.instance.ChangeHealth(-damage);
    }

    public void Hit(int amount, Vector3 other)
    {
        if (isInvincible)
            return;
        if (isKnockbackable)
            StartCoroutine(Knockback(other));
        audioSource.PlayOneShot(hitSound);
        StartCoroutine(invincibleCoroutine());
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log(currentHealth / maxHealth);
        maskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * ((float)currentHealth / (float)maxHealth));
        if (currentHealth <= 0)
        {
            audioSource.PlayOneShot(deadSound);
            Invoke("Dead", deadSound.length);
        }
    }

    IEnumerator Knockback(Vector3 other)
    {
        if (!isInvincible)
        {
            GetComponent<EnemyPatrol>().canMove = false;
            Vector2 knockBackDirection = (other - this.transform.position).normalized;
            if (knockBackDirection.x < 0) knockBackDirection.x = -1;
            else knockBackDirection.x = 1;
            if (knockBackDirection.y < 0) knockBackDirection.y = -1;
            else knockBackDirection.y = 1;
            rb.AddForce(new Vector2(25, 25) * -knockBackDirection, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            rb.velocity = new Vector2(0f, 0f);
            GetComponent<EnemyPatrol>().canMove = true;
        }

    }
    private IEnumerator invincibleCoroutine()
    {
        isInvincible = true;
        for (float i = 0; i < invincibleLength; i += invincibleDeltaTime)
        {
            if (transform.localScale == scale)
            {
                transform.localScale = new Vector3(0, 0, 1);
            }
            else
            {
                transform.localScale = scale;
            }
            yield return new WaitForSeconds(invincibleDeltaTime);
        }
        transform.localScale = scale;
        isInvincible = false;
    }

    void Dead()
    {

        Destroy(transform.parent.gameObject);
    }
}
