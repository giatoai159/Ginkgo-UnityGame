using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PlayerController.instance.Knockback(this.transform));
            PlayerHealthController.instance.ChangeHealth(-1);
        }
    }
}
