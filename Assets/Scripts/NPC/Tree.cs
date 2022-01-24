using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tree : MonoBehaviour
{
    public Text waterCount;
    public Text windCount;
    public Text lightCount;
    public float displayTime;
    public GameObject dialogBox;
    public GameObject victoryText;
    public GameObject directionText;
    float timer;
    bool victory;
    // Start is called before the first frame update
    void Start()
    {
        waterCount.text = PlayerPrefs.GetInt("WaterLevel_collectibles", 0) + "/25";
        windCount.text = PlayerPrefs.GetInt("WindLevel_collectibles", 0) + "/20";
        lightCount.text = PlayerPrefs.GetInt("LightLevel_collectibles", 0) + "/30";
        dialogBox.SetActive(false);
        timer = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0) dialogBox.SetActive(false);
        }
        if (victory && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("LevelSelection");
        }
    }

    public void ReturnElement()
    {
        if (PlayerPrefs.GetInt("WaterLevel_collectibles", 0) > 25 &&
        PlayerPrefs.GetInt("WindLevel_collectibles", 0) > 20 &&
        PlayerPrefs.GetInt("LightLevel_collectibles", 0) > 30)
        {
            PlayerPrefs.SetInt("GameBeaten", 1);
            AudioChange.instance.PlayVictoryMusic();
            victoryText.SetActive(true);
            directionText.SetActive(true);
            victory = true;
        }
        else
        {
            DisplayDialog();
        }
    }

    public void DisplayDialog()
    {
        timer = displayTime;
        dialogBox.SetActive(true);
    }
}
