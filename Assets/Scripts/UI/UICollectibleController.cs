using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectibleController : MonoBehaviour
{
    public static UICollectibleController instance;
    public Image icon;
    public Sprite waterOrb;
    public Sprite windOrb;
    public Sprite lightOrb;
    void Awake()
    {
        instance = this;
    }
    public void UpdateIcon(int _icon)
    {
        switch (_icon)
        {
            case 1: icon.sprite = waterOrb; break;
            case 2: icon.sprite = windOrb; break;
            case 3: icon.sprite = lightOrb; break;
            default: break;
        }
    }
    public void UpdateCollectibleCount()
    {
        var text = GetComponentInChildren<Text>();
        text.text = LevelManager.instance.getCollectedCount.ToString() + " / " + LevelManager.instance.getTotalCollectible.ToString();
    }

}
