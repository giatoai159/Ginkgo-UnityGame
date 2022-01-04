using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    Rigidbody2D rb;
    bool facing;
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
    }
    void Timer()
    {
        if (basicAttackTimer >= 0) basicAttackTimer -= Time.deltaTime;
    }
    #region Basic Attack
    [SerializeField] GameObject basicAttackRight;
    [SerializeField] GameObject basicAttackLeft;
    public float basicAttackCD;
    float basicAttackTimer;
    void BasicAttack()
    {
        if (Input.GetButtonDown("Fire1") && basicAttackTimer <= 0)
        {
            basicAttackTimer = basicAttackCD;
            StartCoroutine(basicAttackCo());
        }
    }

    IEnumerator basicAttackCo()
    {
        var basicAttackSword = Instantiate(facing ? basicAttackLeft : basicAttackRight, new Vector3(transform.position.x + (facing ? -0.36f : 0.36f), transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        basicAttackSword.transform.parent = transform;
        yield return new WaitForSeconds(0.45f);
    }
    #endregion
}
