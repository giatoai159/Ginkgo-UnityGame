using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float respawnTime;
    public Transform respawnPosition;
    int totalCollectible;
    public int getTotalCollectible { get { return totalCollectible; } }
    int collectedCount;
    public int getCollectedCount { get { return collectedCount; } }
    GameObject[] respawnEnemies;
    GameObject[] respawnCollectibles;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        collectedCount = 0;
        respawnEnemies = GameObject.FindGameObjectsWithTag("Respawn");
        respawnCollectibles = GameObject.FindGameObjectsWithTag("Pickup");
        if (respawnCollectibles.Length > 0)
        {
            if (respawnCollectibles[0].GetComponentInChildren<SpriteRenderer>().sprite.name == "Elemental Orbs_0")
                UICollectibleController.instance.UpdateIcon(1);
            if (respawnCollectibles[0].GetComponentInChildren<SpriteRenderer>().sprite.name == "Elemental Orbs_1")
                UICollectibleController.instance.UpdateIcon(2);
            if (respawnCollectibles[0].GetComponentInChildren<SpriteRenderer>().sprite.name == "Elemental Orbs_2")
                UICollectibleController.instance.UpdateIcon(3);
        }
        totalCollectible = respawnCollectibles.Length + respawnEnemies.Length;
        UICollectibleController.instance.UpdateCollectibleCount();
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
        PlayerController.instance.GetComponent<SpriteRenderer>().sortingOrder = 11;
        PlayerController.instance.GetComponent<Animator>().SetBool("Dead", true);
        PlayerController.instance.lockPlayerMovement = true;
        yield return new WaitForSeconds(respawnTime);
        PlayerController.instance.GetComponent<CapsuleCollider2D>().isTrigger = false;
        PlayerController.instance.GetComponent<SpriteRenderer>().sortingOrder = 2;
        PlayerController.instance.GetComponent<Animator>().SetBool("Dead", false);
        PlayerController.instance.lockPlayerMovement = false;

        PlayerController.instance.transform.position = respawnPosition.position;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIHeartController.instance.SetHeart(PlayerHealthController.instance.maxHealth, PlayerHealthController.instance.maxHealth);
        collectedCount = 0;
        RespawnEnemies();
        RespawnCollectibles();
        UICollectibleController.instance.UpdateCollectibleCount();
    }

    void RespawnEnemies()
    {
        foreach (var obj in respawnEnemies)
        {
            obj.SetActive(true);
            obj.GetComponentInChildren<EnemyController>().RestoreHealth();
        }
    }

    void RespawnCollectibles()
    {
        foreach (var obj in respawnCollectibles)
        {
            obj.SetActive(true);
        }
    }

    public void AddCollectible()
    {
        collectedCount++;
    }
}
