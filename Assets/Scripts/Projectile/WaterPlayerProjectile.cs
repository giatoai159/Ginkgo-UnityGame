using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlayerProjectile : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    SpriteRenderer sr;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D bc;
    public float aliveTime = 5;
    float aliveTimer;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        aliveTimer = aliveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveTimer >= 0f) aliveTimer -= Time.deltaTime;
        else
        {
            var contactPoint = new Vector2(transform.position.x + 0.6f, transform.position.y);
            Instantiate(hitEffect, contactPoint, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            other.GetContacts(contacts);
            Vector2 contactPoint = contacts[0].point;
            Instantiate(hitEffect, contactPoint, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var contactPoint = new Vector2(transform.position.x + 0.6f, transform.position.y);
            Instantiate(hitEffect, contactPoint, Quaternion.identity);
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.Hit(2, transform.position);
            sr.enabled = false;
            bc.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
