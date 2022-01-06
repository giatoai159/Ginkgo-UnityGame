using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.ChangeHealth(-9999);
        }
    }
}
