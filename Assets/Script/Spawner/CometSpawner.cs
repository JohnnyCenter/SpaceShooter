using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CometSpawner : MonoBehaviour
{
    [SerializeField]
    private List<int> numberPool;
    [SerializeField]
    private int numberCount;
    private int cometType, spawnTimer;
    [SerializeField]
    private int minTimer, maxTimer;
    playerController player;

    [SerializeField]
    GameObject[] Comets;

    private void Awake()
    {
        numberPool = new List<int>();
        ReloadNumberPool();
        numberCount = 0;
        StartCoroutine("SpawnCycle");
        player = GetComponentInParent<playerController>();
    }

    void ReloadNumberPool()
    {
        for (int n = 0; n < 4; n++)
        {
            numberPool.Add(n);
        }
        Debug.Log("COMET: PoolReloaded");
    }

    void PickNumber()
    {
        int index = Random.Range(0, numberPool.Count - 1);
        cometType = numberPool[index];
        numberPool.RemoveAt(index);
        numberCount += 1;
        Debug.Log("COMET: Picked a number and removed a slot");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PickNumber();
        }

        if (numberCount == 4)
        {
            StartCoroutine("Refill");
        }
    }

    IEnumerator Refill()
    {
        numberCount = 0;
        ReloadNumberPool();
        yield return null;
    }

    void SpawnComet()
    {
        Instantiate(Comets[cometType], transform.position, Quaternion.Euler(new Vector3(0, 0, player.transform.rotation.eulerAngles.z + 180)));
        Debug.Log("COMET: Spawned a Comet");
    }

    IEnumerator SpawnCycle()
    {
        Debug.Log("COMET: CYCLE HAS BEGUN");
        spawnTimer = Random.Range(minTimer, maxTimer);
        yield return new WaitForSeconds(spawnTimer);
        yield return new WaitUntil(() => player.turning == false);
        Debug.Log("COMET: Spawntimer over");
        PickNumber();
        SpawnComet();
    }

    void StartCycle()
    {
        StartCoroutine("SpawnCycle");
    }

    private void OnEnable()
    {
        Comet.StartingCycle += StartCycle;
    }

    private void OnDisable()
    {
        Comet.StartingCycle -= StartCycle;
    }
}
