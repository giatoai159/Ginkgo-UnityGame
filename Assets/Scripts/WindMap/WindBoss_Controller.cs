using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindBoss_Controller : MonoBehaviour
{
    public static WindBoss_Controller instance;
    public float timeBetweenShots;
    public float projectileSpeed;
    float shootingTimer;
    public float specialAbilityCD;
    float specialAbilityTimer;
    bool doneSpecialAbility;
    [SerializeField] BoxCollider2D detectPlayer;
    [SerializeField] GameObject projectile;
    EnemyController enemy;
    GameObject target;
    Rigidbody2D rb;
    [SerializeField] Animator anim;
    int state;
    Vector3 bossOriginalPosition;
    public Vector3 getBossOriginalPosition { get { return bossOriginalPosition; } }
    bool playingVictoryMusic;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyController>();
        state = 1;
        specialAbilityTimer = specialAbilityCD;
        bossOriginalPosition = transform.position;
        playingVictoryMusic = false;
    }
    void Update()
    {
        if (target)
        {
            switch (state)
            {
                case 1:
                    {
                        if (Vector2.Distance(transform.position, target.transform.position) >= 1f)
                            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemy.movementSpeed * Time.deltaTime);
                        if (shootingTimer >= 0f) shootingTimer -= Time.deltaTime;
                        else
                        {
                            anim.SetTrigger("Attack1");
                            shootingTimer = timeBetweenShots;
                            StartCoroutine(ShootingCoroutine(0.75f));
                        }
                        if (specialAbilityTimer >= 0) specialAbilityTimer -= Time.deltaTime;
                        else
                        {
                            state = Random.Range(3, 4);
                            doneSpecialAbility = false;
                        }
                    }
                    break;
                case 2:
                    {
                        var targetHead = new Vector2(target.transform.position.x, target.transform.position.y + 5f);
                        if (!doneSpecialAbility)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, targetHead, enemy.movementSpeed * 7 * Time.deltaTime);
                        }
                        if (Vector2.Distance(transform.position, targetHead) <= 0.1f && doneSpecialAbility == false)
                        {
                            anim.SetTrigger("Attack2");
                            specialAbilityTimer = specialAbilityCD;
                            doneSpecialAbility = true;
                            StartCoroutine(SpecialAbility1Coroutine(0.5f, 2.5f));
                        }
                    }
                    break;
                case 3:
                    {
                        if (doneSpecialAbility == false)
                        {
                            anim.SetTrigger("Attack2");
                            specialAbilityTimer = specialAbilityCD;
                            doneSpecialAbility = true;
                            StartCoroutine(SpecialAbility2Coroutine(0.5f, 2.5f));
                        }
                    }
                    break;
            }
        }
        if (enemy.getCurrentHealth == 0 && playingVictoryMusic == false)
        {
            LevelManager.instance.Victory();
            playingVictoryMusic = true;
        }
    }
    IEnumerator ShootingCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        var shootingProjectile = Instantiate(projectile);
        shootingProjectile.transform.position = new Vector2(transform.position.x, transform.position.y);
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shootingProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        WaterBossProjectile obj = shootingProjectile.GetComponent<WaterBossProjectile>();
        obj.Launch(dir.normalized, projectileSpeed);
    }

    IEnumerator SpecialAbility1Coroutine(float delay, float lockTime)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i <= 180; i += 30)
        {
            var shootingProjectile = Instantiate(projectile, transform.position, Quaternion.AngleAxis(-i, Vector3.forward));
            Vector3 dir = Quaternion.AngleAxis(-i, Vector3.forward) * Vector3.right;
            WaterBossProjectile obj = shootingProjectile.GetComponent<WaterBossProjectile>();
            obj.Launch(dir.normalized, projectileSpeed * 1.5f);
        }
        yield return new WaitForSeconds(lockTime);
        state = 1;
    }

    IEnumerator SpecialAbility2Coroutine(float delay, float lockTime)
    {
        yield return new WaitForSeconds(delay);
        var reverse = Random.value < 0.5;
        for (int i = 63; i <= 83; i += 3)
        {
            var shootingProjectile = Instantiate(projectile, new Vector2(reverse ? 183f : 222f, i), Quaternion.AngleAxis(reverse ? 0 : 180, Vector3.forward));
            Vector3 dir = Quaternion.AngleAxis(reverse ? 0 : 180, Vector3.forward) * Vector3.right;
            WaterBossProjectile obj = shootingProjectile.GetComponent<WaterBossProjectile>();
            obj.Launch(dir.normalized, projectileSpeed * 1.5f);
        }
        yield return new WaitForSeconds(lockTime);
        state = 1;
    }
    public void DetectPlayer(GameObject player)
    {
        if (player)
            target = player;
        else target = null;
    }
}
