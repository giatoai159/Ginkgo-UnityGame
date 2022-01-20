using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayerController.instance.Knockback(this.transform));
            PlayerHealthController.instance.ChangeHealth(-damage);
        }
    }
}
