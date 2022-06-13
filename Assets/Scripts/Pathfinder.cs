using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfigSO;
    List<Transform> wayPoints;
    int currentWayPointIndex = 0;
    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        waveConfigSO = enemySpawner.GetCurrentWave();
        wayPoints = waveConfigSO.GetWayPoints();
        transform.position = wayPoints[currentWayPointIndex].position;
    }

    
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (currentWayPointIndex < wayPoints.Count)
        {
            Vector3 targetPosition = wayPoints[currentWayPointIndex].position;
            float delta = waveConfigSO.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                currentWayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
