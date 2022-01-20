using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBossController : MonoBehaviour
{
    public static LightBossController instance;
    public float timeBetweenShots;
    public float projectileSpeed;
    float shootingTimer;
    public float specialAbilityCD;
    float specialAbilityTimer;
    bool doneSpecialAbility;
    [SerializeField] GameObject projectile;
    EnemyController enemy;
    GameObject target;
    [SerializeField] Animator anim;
    Vector3 middle;
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
        enemy = GetComponent<EnemyController>();
        state = 1;
        middle = new Vector3(575f, 40f, 0);
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
                            state = Random.Range(2, 3);
                            doneSpecialAbility = false;
                        }
                    }
                    break;
                case 2:
                    {
                        transform.position = Vector2.MoveTowards(transform.position, middle, enemy.movementSpeed * 10 * Time.deltaTime);
                        if (Vector2.Distance(transform.position, middle) <= 0.1f && doneSpecialAbility == false)
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
            PlayerPrefs.SetInt("shield", 1);
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
        var reverse = (Random.value < 0.5f);
        for (int i = 0; i < 360; i += 15)
        {
            var shootingProjectile = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y), Quaternion.AngleAxis(reverse ? i : -i, Vector3.forward));
            Vector3 dir = Quaternion.AngleAxis(reverse ? i : -i, Vector3.forward) * Vector3.right;
            WaterBossProjectile obj = shootingProjectile.GetComponent<WaterBossProjectile>();
            obj.Launch(dir.normalized, projectileSpeed * 1.5f);
            yield return new WaitForSeconds(0.025f);
        }
        yield return new WaitForSeconds(lockTime);
        state = 1;
    }

    IEnumerator SpecialAbility2Coroutine(float delay, float lockTime)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 189; i <= 217; i += 2)
        {
            var shootingProjectile = Instantiate(projectile, new Vector2(i, 11f), Quaternion.AngleAxis(-90, Vector3.forward));
            Vector3 dir = Quaternion.AngleAxis(-90, Vector3.forward) * Vector3.right;
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
