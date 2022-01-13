using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FadeScreen.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLS()
    {
        StartCoroutine(LoadLSCo());
    }

    IEnumerator LoadLSCo()
    {
        FadeScreen.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / FadeScreen.instance.fadeSpeed) - 0.05f);
        SceneManager.LoadScene("LevelSelection");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
