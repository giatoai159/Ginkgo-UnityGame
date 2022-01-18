using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropDown;
    public Toggle isFullscreen;
    public Slider bgmSlider;
    public Slider sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + " Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
        if (Screen.fullScreen)
            isFullscreen.isOn = true;
        else isFullscreen.isOn = false;
        if (PlayerPrefs.HasKey("bgm"))
        {
            SetBGM(PlayerPrefs.GetFloat("bgm"));
            bgmSlider.value = PlayerPrefs.GetFloat("bgm", 0f);
        }
        if (PlayerPrefs.HasKey("sfx"))
        {
            SetSFX(PlayerPrefs.GetFloat("sfx"));
            bgmSlider.value = PlayerPrefs.GetFloat("sfx", 0f);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("bgm", volume);
        PlayerPrefs.SetFloat("bgm", volume);
    }

    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfx", volume);
    }

}
