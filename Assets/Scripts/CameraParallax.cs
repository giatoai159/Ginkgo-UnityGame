using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    private float length, startpos, startposY;
    public bool followY;
    public GameObject cam;
    public float parallexEffect;
    void Start()
    {
        startpos = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = (cam.transform.position.x * parallexEffect);
        if (followY)
        {
            float distY = (cam.transform.position.y * parallexEffect);
            transform.position = new Vector3(startpos + dist, startposY + distY, transform.position.z);
        }
        else transform.position = new Vector3(startpos + dist, startposY, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
