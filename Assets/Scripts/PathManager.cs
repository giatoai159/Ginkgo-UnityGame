using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0;i < transform.childCount - 1;i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }
    }

    public Vector3 GetSpot(int index)
    {
        return transform.GetChild(index).position;
    }

    public int Count()
    {
        return transform.childCount;
    }
}
