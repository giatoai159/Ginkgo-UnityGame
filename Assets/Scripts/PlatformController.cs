using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float waitTime;
    public float movementSpeed;
    float waitTimer;
    public PathManager pathManager;
    int nextSpot;
    Transform objParent;
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = 0f;
        nextSpot = 1;
        transform.position = pathManager.GetSpot(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, pathManager.GetSpot(nextSpot)) < 0.2f)
        {
            if (waitTimer >= waitTime)
            {
                nextSpot = (nextSpot + 1) % pathManager.Count();
                waitTimer = 0;
            }
            else
            {
                waitTimer += Time.deltaTime;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, pathManager.GetSpot(nextSpot), movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objParent = collision.transform.parent;
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(objParent);
    }
}
