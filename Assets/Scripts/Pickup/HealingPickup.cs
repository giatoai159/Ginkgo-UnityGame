using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickup : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
    [SerializeField] AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerHealthController.instance.needHealing)
        {
            PlayerSoundController.instance.PlaySound(pickupSound);
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            PlayerHealthController.instance.ChangeHealth(4);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
