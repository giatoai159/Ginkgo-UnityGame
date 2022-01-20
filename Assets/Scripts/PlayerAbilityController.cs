using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    public static PlayerAbilityController instance;
    Rigidbody2D rb;
    bool facing;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        facing = PlayerController.instance.getFacing;
        BasicAttack();
        IcicleShooting();
        Shielding();
    }
    void Timer()
    {
        if (basicAttackTimer >= 0) basicAttackTimer -= Time.deltaTime;
        if (shootingTimer >= 0) shootingTimer -= Time.deltaTime;
        if (shieldTimer >= 0 && !shieldActive) shieldTimer -= Time.deltaTime;
    }
    #region Basic Attack
    [SerializeField] GameObject basicAttackRight;
    [SerializeField] GameObject basicAttackLeft;
    [SerializeField] AudioClip basicAttackSound;
    public float basicAttackCD;
    float basicAttackTimer;
    void BasicAttack()
    {
        if (Input.GetButtonDown("Fire1") && basicAttackTimer <= 0)
        {
            basicAttackTimer = basicAttackCD;
            PlayerSoundController.instance.PlaySound(basicAttackSound);
            var basicAttackSword = Instantiate(facing ? basicAttackLeft : basicAttackRight, new Vector3(transform.position.x + (facing ? -0.36f : 0.36f), transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
            basicAttackSword.transform.parent = transform;
        }
    }
    #endregion
    #region Water Icicle
    [SerializeField] GameObject projectile;
    public float projectileSpeed;
    public bool canShoot;
    public float shootingCD;
    float shootingTimer;
    public float getShootingTimer { get { return shootingTimer; } }
    void IcicleShooting()
    {
        if (Input.GetButtonDown("Fire2") && shootingTimer <= 0 && canShoot)
        {
            shootingTimer = shootingCD;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (Vector2)(worldMousePos - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var icicle = Instantiate(projectile);
            icicle.transform.position = new Vector2(transform.position.x, transform.position.y);
            icicle.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            WaterPlayerProjectile obj = icicle.GetComponent<WaterPlayerProjectile>();
            obj.Launch(dir.normalized, projectileSpeed);
        }
    }
    #endregion
    #region Holy Shield
    [SerializeField] GameObject shield;
    public bool canShield;
    GameObject shieldActive;
    public float shieldCD;
    float shieldTimer;
    public float getShieldTimer { get { return shieldTimer; } }
    void Shielding()
    {
        if (Input.GetKeyDown(KeyCode.E) && shieldTimer <= 0 && canShield && !shieldActive)
        {
            shieldTimer = shieldCD;
            shieldActive = Instantiate(shield, new Vector2(transform.position.x, transform.position.y - 0.3f), Quaternion.identity);
            shieldActive.transform.parent = transform;
        }
    }
    #endregion
}
