using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShield : MonoBehaviour
{
    public int shieldMaxHealth;
    int shieldCurrentHealth;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deadSound;
    [SerializeField] CircleCollider2D cc2d;
    [SerializeField] Rigidbody2D rb;
    bool shieldDestroyed;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            int damage = other.GetComponent<EnemyController>().damage;
            ShieldHurt(damage);
        }
        if (other.CompareTag("Spike"))
        {
            ShieldHurt(1);
        }
        if (other.CompareTag("Projectile"))
        {
            ShieldHurt(1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        shieldCurrentHealth = shieldMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldCurrentHealth > 0)
        {
            PlayerHealthController.instance.invincibleState = true;
        }
        else
        {
            if (!shieldDestroyed)
                StartCoroutine(ShieldDestroyed());
        }
    }

    IEnumerator ShieldDestroyed()
    {
        shieldDestroyed = true;
        PlayerSoundController.instance.PlaySound(deadSound);
        yield return new WaitForSeconds(0.2f);
        PlayerHealthController.instance.invincibleState = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(transform.parent.gameObject);
    }

    void ShieldHurt(int amount)
    {
        shieldCurrentHealth -= amount;
        PlayerSoundController.instance.PlaySound(hitSound);
    }
}
