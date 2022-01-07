using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float waitTime;
    public AnimationCurve animCurve;
    public PathManager pathManager;
    int nextSpot;
    float movementSpeed;
    float waitTimer;
    float maxDistance;
    float currentDistance;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = 0;
        movementSpeed = GetComponent<EnemyController>().movementSpeed;
        nextSpot = 1;
        transform.position = pathManager.GetSpot(0);
        currentDistance = Vector2.Distance(transform.position, pathManager.GetSpot(nextSpot));
        maxDistance = Vector2.Distance(transform.position, pathManager.GetSpot(nextSpot));
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector2.Distance(transform.position, pathManager.GetSpot(nextSpot));
        float percent = 1 - (currentDistance / maxDistance);
        if (Vector2.Distance(transform.position, pathManager.GetSpot(nextSpot)) < 0.2f)
        {
            if (waitTimer >= waitTime)
            {
                nextSpot = (nextSpot + 1) % pathManager.Count();
                waitTimer = 0;
                maxDistance = Vector2.Distance(transform.position, pathManager.GetSpot(nextSpot));
            }
            else
            {
                waitTimer += Time.deltaTime;
            }

        }
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, pathManager.GetSpot(nextSpot), animCurve.Evaluate(percent) * movementSpeed * Time.deltaTime);
        }

    }

}
