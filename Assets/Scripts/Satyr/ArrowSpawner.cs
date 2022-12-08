using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : Singleton<ArrowSpawner>
{
    [SerializeField] int arrowsToSpawn = 10;
    [SerializeField] float spawningDelay = 0.26f;
    [SerializeField] GameObject arrowPrefab;
    public static void SpawnArrows()
    {
        Instance.StartCoroutine(Instance.StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        int toSpawn = arrowsToSpawn;
        for (int i = 0; i < arrowsToSpawn; i++)
        {
            Vector3 point = ChooseRandomSpawnPoint();
            GameObject spawnedArrow = Instantiate(arrowPrefab, point, Quaternion.LookRotation(Vector3.forward, Vector3.down));
            yield return new WaitForSeconds(spawningDelay);
        }
    }

    private Vector3 ChooseRandomSpawnPoint()
    {
        Vector3 chosenPoint = new Vector3(UnityEngine.Random.Range(transform.position.y - (transform.localScale.x / 2f), 
                                transform.position.y + (transform.localScale.x / 2f)), 
                                transform.position.y, 0); 
        return chosenPoint;
    }
}
