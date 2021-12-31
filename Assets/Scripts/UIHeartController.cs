using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartController : MonoBehaviour
{
    public static UIHeartController instance;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject halfHeartPrefab;
    [SerializeField] GameObject noHeartPrefab;
    void Awake()
    {
        instance = this;
    }

    public void SetHeart(int health, int maxHealth)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < Mathf.CeilToInt((float)maxHealth / 2.0f); i++)
        {
            if (i * 2 + 1 < health)
            {
                GameObject heart = Instantiate(heartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
            }
            else if (i * 2 + 1 == health)
            {
                GameObject heart = Instantiate(halfHeartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
            }
            else
            {
                GameObject heart = Instantiate(noHeartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
            }
        }
    }
}
