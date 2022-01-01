using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float waitTime;
    public AnimationCurve animCurve;
    public Transform[] patrolSpots;
    int numberOfSpots;
    int patrolIndex;
    Transform currentPatrolSpot;
    float movementSpeed;
    float waitTimer;
    float maxDistance;
    float currentDistance;
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = 0;
        movementSpeed = GetComponent<EnemyController>().movementSpeed;
        patrolIndex = 1;
        currentPatrolSpot = patrolSpots[patrolIndex];
        transform.position = patrolSpots[0].position;
        currentDistance = Vector2.Distance(transform.position, currentPatrolSpot.position);
        maxDistance = Vector2.Distance(transform.position, currentPatrolSpot.position);
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector2.Distance(transform.position, currentPatrolSpot.position);
        float percent = 1 - (currentDistance / maxDistance);
        if (Vector2.Distance(transform.position, currentPatrolSpot.position) < 0.2f)
        {
            if (waitTimer >= waitTime)
            {
                if (patrolIndex + 1 < patrolSpots.Length)
                    patrolIndex++;
                else patrolIndex = 0;
                currentPatrolSpot = patrolSpots[patrolIndex];
                waitTimer = 0;
                maxDistance = Vector2.Distance(transform.position, currentPatrolSpot.position);
            }
            else
            {
                waitTimer += Time.deltaTime;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, currentPatrolSpot.position, animCurve.Evaluate(percent) * movementSpeed * Time.deltaTime);

    }

}
