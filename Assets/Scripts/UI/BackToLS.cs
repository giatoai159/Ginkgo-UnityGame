using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLS : MonoBehaviour
{
    void OnCollisionEnter2D()
    {
        if (!PlayerPrefs.HasKey("SawIntro"))
            PlayerPrefs.SetInt("SawIntro", 1);
        StartCoroutine(LoadLSCo());
    }

    IEnumerator LoadLSCo()
    {
        FadeScreen.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / FadeScreen.instance.fadeSpeed) - 0.05f);
        SceneManager.LoadScene("LevelSelection");
    }
}
