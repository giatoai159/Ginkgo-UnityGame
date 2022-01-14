using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    public float projectileSpeed;
    public float timeBetweenShots;
    float shootingTimer;
    GameObject target;
    void Update()
    {
        if (target)
        {
            if (shootingTimer >= 0f) shootingTimer -= Time.deltaTime;
            else
            {
                shootingTimer = timeBetweenShots;
                var shootingProjectile = Instantiate(projectile);
                shootingProjectile.transform.position = new Vector2(transform.position.x, transform.position.y);
                Vector3 dir = target.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                shootingProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                WaterBossProjectile obj = shootingProjectile.GetComponent<WaterBossProjectile>();
                obj.Launch(dir.normalized, projectileSpeed);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DetectPlayer(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DetectPlayer(null);
        }
    }

    void DetectPlayer(GameObject player)
    {
        if (player)
            target = player;
        else target = null;
    }
}
