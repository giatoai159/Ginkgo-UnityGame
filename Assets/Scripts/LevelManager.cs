using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float respawnTime;
    public Transform respawnPosition;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = true;
        PlayerController.instance.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        PlayerController.instance.GetComponent<SpriteRenderer>().sortingOrder = 11;
        PlayerController.instance.GetComponent<Animator>().SetBool("Dead", true);
        yield return new WaitForSeconds(respawnTime);
        PlayerController.instance.transform.position = respawnPosition.position;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = false;
        PlayerController.instance.GetComponent<SpriteRenderer>().sortingOrder = 1;
        PlayerController.instance.GetComponent<Animator>().SetBool("Dead", false);
        UIHeartController.instance.SetHeart(PlayerHealthController.instance.maxHealth, PlayerHealthController.instance.maxHealth);
    }
}
