using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth;
    public int currentHealth;
    public float invincibleLength;
    public float invincibleDeltaTime;
    bool isInvincible = false;
    public bool invincibleState { get { return isInvincible; } }
    SpriteRenderer sr;
    Animator anim;
    [SerializeField] AudioClip hurtSound;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        UIHeartController.instance.SetHeart(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            StartCoroutine(PlayerController.instance.Knockback(this.transform));
            StartCoroutine(invincibleCoroutine());
            PlayerSoundController.instance.PlaySound(hurtSound);
            anim.SetTrigger("Hurt");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHeartController.instance.SetHeart(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            LevelManager.instance.RespawnPlayer();
        }
    }

    private IEnumerator invincibleCoroutine()
    {
        isInvincible = true;
        for (float i = 0; i < invincibleLength; i += invincibleDeltaTime)
        {
            if (sr.color.a == 1.0f)
            {
                Flashing(0.2f);
            }
            else
            {
                Flashing(1.0f);
            }
            yield return new WaitForSeconds(invincibleDeltaTime);
        }
        Flashing(1.0f);
        isInvincible = false;
    }

    void Flashing(float alpha)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }
}
