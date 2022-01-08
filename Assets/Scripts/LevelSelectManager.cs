using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;

    public Image fadeScreen;
    public float fadeSpeed = 0.5f;
    bool fadeToBlack, fadeFromBlack;
    // Start is called before the first frame update
    void Start()
    {
        level1.onClick.AddListener(LoadLevel1);
        level2.onClick.AddListener(LoadLevel2);
        level3.onClick.AddListener(LoadLevel3);
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
                fadeScreen.gameObject.SetActive(false);
            }
        }
        if (fadeFromBlack)
        {
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeFromBlack = false;
                fadeScreen.gameObject.SetActive(false);
            }
        }
    }

    void LoadLevel1()
    {
        StartCoroutine(LoadLevel1Co());
    }

    IEnumerator LoadLevel1Co()
    {
        FadeToBlack();
        yield return new WaitForSeconds((1f / fadeSpeed) - 0.15f);
        SceneManager.LoadScene("WaterLevel");
    }
    void LoadLevel2()
    {
        Debug.Log("Level 2 loaded");
    }
    void LoadLevel3()
    {
        Debug.Log("Level 3 loaded");
    }

    public void FadeToBlack() { fadeFromBlack = false; fadeToBlack = true; }
    public void FadeFromBlack() { fadeFromBlack = true; fadeToBlack = false; }
}
