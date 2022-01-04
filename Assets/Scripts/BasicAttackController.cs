using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackController : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] LayerMask hitLayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            var hitEffectObj = Instantiate(hitEffect, new Vector3(transform.position.x + (PlayerController.instance.getFacing ? -.4f : .4f), transform.position.y, transform.position.z), Quaternion.identity);
            other.GetComponent<EnemyController>().Hit(1, transform.position);
        }
    }
}
