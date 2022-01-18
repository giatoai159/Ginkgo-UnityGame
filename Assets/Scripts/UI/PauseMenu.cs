using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButtons;
    public GameObject settingsMenu;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        if (isPaused)
        {
            PlayerController.instance.enabled = true; // Enable player input
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            PlayerController.instance.enabled = false; // Disable player input
            isPaused = true;
            pauseMenu.SetActive(true);
            pauseButtons.SetActive(true);
            settingsMenu.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void GoToSettings()
    {
        pauseButtons.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelection");
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Interface");
        Time.timeScale = 1f;
    }
}
