using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    // public GameObject timeText
    void Start()
    {
        var level1Collect = level1.transform.Find("Collect Text").GetComponent<Text>();
        if (PlayerPrefs.HasKey("WaterLevel_collectibles"))
        {
            level1Collect.text = "Collected: " + PlayerPrefs.GetInt("WaterLevel_collectibles") + "/31";
        }
        else level1Collect.text = "Collected: 0/31";
        var level1Time = level1.transform.Find("Time Text").GetComponent<Text>();
        if (PlayerPrefs.HasKey("WaterLevel_time"))
        {
            var time = PlayerPrefs.GetFloat("WaterLevel_time");
            var before = Mathf.Floor(time / 60);
            var after = Mathf.Floor(time % 60);
            if (before < 10)
                level1Time.text = "Best Time: 0" + before + ":" + after;
            else level1Time.text = "Best Time: " + before + ":" + after;
        }
        else level1Time.text = "Best Time: 0s";
    }
    public void LoadLevel1()
    {
        StartCoroutine(LoadLevel1Co());
    }

    IEnumerator LoadLevel1Co()
    {
        FadeScreen.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / FadeScreen.instance.fadeSpeed) - 0.05f);
        SceneManager.LoadScene("WaterLevel");
    }
    public void LoadLevel2()
    {
        Debug.Log("Level 2 loaded");
    }
    public void LoadLevel3()
    {
        Debug.Log("Level 3 loaded");
    }
}
