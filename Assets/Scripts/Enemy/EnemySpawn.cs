using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
   [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<GameObject> enemyList;

   void Start()
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
