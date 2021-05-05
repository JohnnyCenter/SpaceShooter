using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFreeRoamSpawner : MonoBehaviour
{
    Vector3 spawnPos;
    [SerializeField]
    private int Dificulty = 1, deathTimer;
    [SerializeField]
    GameObject[] Enemies;
    private int enemyType;
    private bool invisible, spawning;

    private void Awake()
    {
        Dificulty = 1;
        spawnPos = transform.position;
        deathTimer = 20;
        spawning = false;
        Debug.Log(gameObject.transform.childCount);
    }

    private void Start()
    {
        PickEnemy();
    }

    private void OnBecameVisible()
    {
        invisible = false;
    }

    private void OnBecameInvisible()
    {
        invisible = true;
    }

    private void Update()
    {
        if(gameObject.transform.childCount == 0 && spawning == false)
        {
            StartCoroutine("SpawnCooldown");
        }
    }

    void PickEnemy()
    {
        enemyType = Random.Range(0, Dificulty+1);
        Debug.Log("Picking enemy: " + enemyType);
    }

    void SpawnEnemy()
    {
        var myEnemy = Instantiate(Enemies[enemyType], new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);
        myEnemy.transform.parent = gameObject.transform;
    }

    IEnumerator SpawnCooldown()
    {
        spawning = true;
        Dificulty += 1;
        Debug.Log("StartingCooldown on enemyFreeRoamSpawner");
        yield return new WaitForSeconds(deathTimer);
        yield return new WaitUntil(() => invisible == true);
        PickEnemy();
        SpawnEnemy();
        spawning = true;
    }
}
