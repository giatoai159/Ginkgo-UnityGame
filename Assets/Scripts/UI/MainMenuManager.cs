using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainButtons;
    public GameObject settingsMenu;
    public GameObject helpMenu;
    // Start is called before the first frame update
    void Start()
    {
        FadeScreen.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToSettings()
    {
        mainButtons.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackFromSettings()
    {
        mainButtons.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void ShowHelp()
    {
        if (helpMenu.activeSelf) { helpMenu.SetActive(false); } else { helpMenu.SetActive(true); }
    }

    public void ResetGameSave()
    {
        PlayerPrefs.DeleteKey("WaterLevel_time");
        PlayerPrefs.DeleteKey("WaterLevel_collectibles");
        PlayerPrefs.DeleteKey("WindLevel_time");
        PlayerPrefs.DeleteKey("WindLevel_collectibles");
        PlayerPrefs.DeleteKey("LightLevel_time");
        PlayerPrefs.DeleteKey("LightLevel_collectibles");
        PlayerPrefs.DeleteKey("doubleJump");
        PlayerPrefs.DeleteKey("dash");
        mainButtons.SetActive(true);
        settingsMenu.SetActive(false);
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
