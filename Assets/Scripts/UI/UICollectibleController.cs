using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectibleController : MonoBehaviour
{
    public static UICollectibleController instance;

    void Awake()
    {
        instance = this;
    }
    public void UpdateCollectibleCount()
    {
        var text = GetComponentInChildren<Text>();
        text.text = LevelManager.instance.getCollectedCount.ToString() + " / " + LevelManager.instance.getTotalCollectible.ToString();
    }

}
