using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    [SerializeField]
    private List<int> numberPool;
    private int numberCount;

    [SerializeField]
    GameObject[] Comets;

    private void Awake()
    {
        numberPool = new List<int>();
        ReloadNumberPool();
        numberCount = 0;
    }

    void ReloadNumberPool()
    {
        for (int n = 0; n < 4; n++)
        {
            numberPool.Add(n);
        }
    }

    void PickNumber()
    {
        int index = Random.Range(0, numberPool.Count - 1);
        int i = numberPool[index];
        numberPool.RemoveAt(index);
        numberCount += 1;
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
}
