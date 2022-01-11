using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBossRoom_WindMap : MonoBehaviour
{
    [SerializeField] GameObject blockTile;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioChange.instance.PlayBossMusic();
            blockTile.SetActive(true);
            WindBoss_Controller.instance.DetectPlayer(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioChange.instance.PlayLevelMusic();
            blockTile.SetActive(false);
            WindBoss_Controller.instance.DetectPlayer(null);
            WindBoss_Controller.instance.transform.position = WindBoss_Controller.instance.getBossOriginalPosition;
        }
    }
}
