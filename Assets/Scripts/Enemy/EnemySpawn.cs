using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
   [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<GameObject> enemyList;
   

    bool isSpawning = false;
    public float minTime = 5.0f;
    public float maxTime = 15.0f;

    //IEnumerator SpawnObject(float seconds, int point)
    //{
    //    Debug.Log("Waiting for " + seconds + " seconds");

    //    yield return new WaitForSeconds(seconds);
    //    Instantiate( enemyPrefab, spawnPoints[point].position, spawnPoints[point].rotation);

    //    //We've spawned, so now we could start another spawn     
    //    isSpawning = false;
    //}

    //void Update()
    //{
    //    //We only want to spawn one at a time, so make sure we're not already making that call
    //    if (!isSpawning)
    //    {
    //        isSpawning = true; //Yep, we're going to spawn

    //        int pointindex= Random.Range(0, spawnPoints.Count);
    //        StartCoroutine(SpawnObject( Random.Range(minTime, maxTime),pointindex));
    //    }
    //}
    void Awake()
    {
        
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        foreach (Transform spawnPt in spawnPoints)
        {
            enemyList.Add(Instantiate(enemyPrefab, spawnPt));
        }
    }
}
