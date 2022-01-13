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
        #region Level 1
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
        #endregion

        #region Level 2
        var level2Collect = level2.transform.Find("Collect Text").GetComponent<Text>();
        if (PlayerPrefs.HasKey("WindLevel_collectibles"))
        {
            level2Collect.text = "Collected: " + PlayerPrefs.GetInt("WindLevel_collectibles") + "/31";
        }
        else level2Collect.text = "Collected: 0/31";
        var level2Time = level2.transform.Find("Time Text").GetComponent<Text>();
        if (PlayerPrefs.HasKey("WindLevel_time"))
        {
            var time = PlayerPrefs.GetFloat("WindLevel_time");
            var before = Mathf.Floor(time / 60);
            var after = Mathf.Floor(time % 60);
            if (before < 10)
                level2Time.text = "Best Time: 0" + before + ":" + after;
            else level2Time.text = "Best Time: " + before + ":" + after;
        }
        else level2Time.text = "Best Time: 0s";
        #endregion
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
        StartCoroutine(LoadLevel2Co());
    }

    IEnumerator LoadLevel2Co()
    {
        FadeScreen.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / FadeScreen.instance.fadeSpeed) - 0.05f);
        SceneManager.LoadScene("WindLevel");
    }
    public void LoadLevel3()
    {
        Debug.Log("Level 3 loaded");
    }

    public void Back()
    {
        StartCoroutine(BackCo());
    }

    IEnumerator BackCo()
    {
        FadeScreen.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / FadeScreen.instance.fadeSpeed) - 0.05f);
        SceneManager.LoadScene("Interface");
    }
}
