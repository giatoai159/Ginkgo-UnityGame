using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBossRoom : MonoBehaviour
{
    [SerializeField] GameObject blockTile;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioChange.instance.PlayBossMusic();
            blockTile.SetActive(true);
            WaterBossController.instance.DetectPlayer(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioChange.instance.PlayLevelMusic();
            blockTile.SetActive(false);
            WaterBossController.instance.DetectPlayer(null);
            WaterBossController.instance.transform.position = WaterBossController.instance.getBossOriginalPosition;
        }
    }
}
