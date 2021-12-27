using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth;
    int currentHealth;
    public float invincibleLength;
    public float invincibleDeltaTime;
    bool isInvincible = false;
    SpriteRenderer sr;
    Animator anim;
    [SerializeField] UIHeartController uiheart;
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
        uiheart.SetHeart(currentHealth, maxHealth);
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
            StartCoroutine(becomeInvincible());
            PlayerSoundController.instance.PlaySound(hurtSound);
            anim.SetTrigger("Hurt");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        uiheart.SetHeart(currentHealth, maxHealth);
    }

    private IEnumerator becomeInvincible()
    {
        isInvincible = true;
        for (float i = 0; i < invincibleLength; i += invincibleDeltaTime)
        {
            if (sr.color.a == 1.0f)
            {
                Flashing(0.0f);
            }
            else
            {
                Flashing(1.0f);
            }
            // TODO: add any logic we want here
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
