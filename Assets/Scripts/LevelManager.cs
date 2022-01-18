using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float respawnTime;
    public Transform respawnPosition;
    int totalCollectible;
    public int getTotalCollectible { get { return totalCollectible; } }
    int collectedCount;
    public int getCollectedCount { get { return collectedCount; } }
    float timeInLevel;
    GameObject[] respawnEnemies;
    GameObject[] respawnCollectibles;
    [SerializeField] GameObject levelCompleteText;
    [SerializeField] GameObject directionText;

    bool victory;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.canDoubleJump = System.Convert.ToBoolean(PlayerPrefs.GetInt("doubleJump", 0));
        PlayerController.instance.canDash = System.Convert.ToBoolean(PlayerPrefs.GetInt("dash", 0));
        timeInLevel = 0f;
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
        if (victory && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("LevelSelection");
        }
        timeInLevel += Time.deltaTime;
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
        timeInLevel = 0f;
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

    public void Victory()
    {
        AudioChange.instance.PlayVictoryMusic();
        StartCoroutine(LevelComplete());
    }
    IEnumerator LevelComplete()
    {
        victory = true;
        levelCompleteText.SetActive(true);
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_collectibles"))
        {
            if (collectedCount > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_collectibles"))
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_collectibles", collectedCount);
        }
        else PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_collectibles", collectedCount);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
        else PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_beaten"))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_beaten", 1);
        }
        yield return new WaitForSeconds(3f);
        directionText.SetActive(true);
    }
}
