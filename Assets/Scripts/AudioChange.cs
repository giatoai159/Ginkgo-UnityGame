using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChange : MonoBehaviour
{
    public static AudioChange instance;
    public AudioClip levelMusic;
    public AudioClip bossMusic;
    public AudioClip victoryMusic;
    [SerializeField] AudioSource audioSource;
    void Awake()
    {
        instance = this;
    }
    public void PlayBossMusic()
    {
        audioSource.clip = bossMusic;
        audioSource.Play();
    }
    public void PlayLevelMusic()
    {
        audioSource.clip = levelMusic;
        audioSource.Play();
    }
    public void PlayVictoryMusic()
    {
        audioSource.clip = victoryMusic;
        audioSource.Play();
    }
}
