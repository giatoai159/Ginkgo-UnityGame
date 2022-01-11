using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen instance;
    public Image fadeScreen;
    public float fadeSpeed = 0.5f;
    bool fadeToBlack, fadeFromBlack;
    void Awake() { instance = this; }
    // Start is called before the first frame update
    void Start()
    {
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
    public void FadeToBlack() { fadeFromBlack = false; fadeToBlack = true; }
    public void FadeFromBlack() { fadeFromBlack = true; fadeToBlack = false; }
}
