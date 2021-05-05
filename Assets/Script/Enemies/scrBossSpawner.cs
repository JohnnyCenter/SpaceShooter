using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private void SpawnBoss()
    {
        print("Boss is spawned!");
        Instantiate(boss, transform.position, Quaternion.identity, null);
    }
    private void OnEnable()
    {
        Portal.OnAllMoonsVisited += SpawnBoss;
    }
    private void OnDisable()
    {
        Portal.OnAllMoonsVisited -= SpawnBoss;
    }
}
