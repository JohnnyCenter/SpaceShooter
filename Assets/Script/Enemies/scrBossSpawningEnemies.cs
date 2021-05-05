using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBossSpawningEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] minions;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float timeBetweenSpawns;

    private void OnBecameVisible()
    {
        StartCoroutine(SpawnMinion(timeBetweenSpawns));
    }
    private int GetRandomMinion()
    {
        int returnValue = Random.Range(0, minions.Length);
        return returnValue;
    }
    private void InstantiateMinion(int index)
    {
        print("Boss spawning enemy");
        GameObject newMinion = Instantiate(minions[index], spawnLocation.transform.position, Quaternion.identity, null);
        StartCoroutine(SpawnMinion(timeBetweenSpawns));
    }
    IEnumerator SpawnMinion(float _timeBetweenSpawns)
    {
        yield return new WaitForSeconds(_timeBetweenSpawns);
        InstantiateMinion(GetRandomMinion());
    }
}
