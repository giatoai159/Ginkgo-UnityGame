using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickup : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
    [SerializeField] AudioClip pickupSound;
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
        if (other.tag == "Player")
        {
            PlayerSoundController.instance.PlaySound(pickupSound);
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            LevelManager.instance.AddCollectible();
            UICollectibleController.instance.UpdateCollectibleCount();
            transform.parent.gameObject.SetActive(false);
        }
    }
}
