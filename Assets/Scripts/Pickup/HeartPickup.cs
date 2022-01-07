using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
    [SerializeField] AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerSoundController.instance.PlaySound(pickupSound);
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            PlayerHealthController.instance.AddMaxHealth();
            transform.parent.gameObject.SetActive(false);
        }
    }
}
