using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
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
            Time.timeScale = 0f;
        }
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelection");
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
