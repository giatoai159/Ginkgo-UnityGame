using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBossRoom_LightMap : MonoBehaviour
{
    [SerializeField] GameObject blockTile;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioChange.instance.PlayBossMusic();
            blockTile.SetActive(true);
            LightBossController.instance.DetectPlayer(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioChange.instance.PlayLevelMusic();
            blockTile.SetActive(false);
            LightBossController.instance.DetectPlayer(null);
            LightBossController.instance.transform.position = LightBossController.instance.getBossOriginalPosition;
        }
    }
}
