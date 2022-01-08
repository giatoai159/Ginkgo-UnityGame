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
    public GameObject Sprite;
    public Image maskImage;
    float originalSize;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deadSound;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector3 scale;
    AudioSource audioSource;
    [SerializeField] GameObject deadEffect;
    [SerializeField] GameObject dropItem;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        originalSize = maskImage.rectTransform.rect.width;
        rb = GetComponent<Rigidbody2D>();
        scale = Sprite.transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        maskImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * ((float)currentHealth / (float)maxHealth));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && currentHealth > 0)
            PlayerHealthController.instance.ChangeHealth(-damage);
    }

    public void Hit(int amount, Vector3 other)
    {
        if (isInvincible)
            return;
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            audioSource.PlayOneShot(deadSound);
            Invoke("Dead", deadSound.length);
        }
        if (isKnockbackable) StartCoroutine(Knockback(other));
        audioSource.PlayOneShot(hitSound);
        StartCoroutine(invincibleCoroutine());
        UpdateHealthBar();
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
            rb.AddForce(new Vector2(15, 15) * -knockBackDirection, ForceMode2D.Impulse);
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
            if (Sprite.transform.localScale == scale)
            {
                Sprite.transform.localScale = new Vector3(0, 0, 1);
            }
            else
            {
                Sprite.transform.localScale = scale;
            }
            yield return new WaitForSeconds(invincibleDeltaTime);
        }
        Sprite.transform.localScale = scale;
        isInvincible = false;
    }

    void Dead()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        currentHealth = maxHealth;
        UpdateHealthBar();
        Sprite.transform.localScale = scale;
        isInvincible = false;
        Instantiate(dropItem, transform.position, Quaternion.identity);
        transform.parent.gameObject.SetActive(false);
    }
}
