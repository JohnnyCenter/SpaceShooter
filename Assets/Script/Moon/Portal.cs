using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Portal : MonoBehaviour
{
    public static Action OnAllMoonsVisited;
    private bool bossIsSpawned;
    [SerializeField]
    int moonCounter, moonGoal; [Tooltip("Set when the Portal should open. When MoonCounter is the same as MoonGoal so will the portal open")]
    bool portalOpen;
    public static event Action PlayerEnteredPortal, PortalOpen;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        moonCounter = 0;
        bossIsSpawned = false;
    }
    private void BossIsDestroyed()
    {
        OpenPortal();
    }
    private void OnEnable()
    {
        Moon.MoonCompleted += AddToMoonCounter;
        scrEnemyStats.OnBossDestroyed += BossIsDestroyed;
    }

    private void OnDisable()
    {
        scrEnemyStats.OnBossDestroyed -= BossIsDestroyed;
        Moon.MoonCompleted -= AddToMoonCounter;
    }

    private void Update()
    {
        if(moonCounter >= moonGoal)
        {
            if(!bossIsSpawned)
            {
                bossIsSpawned = true;
                OnAllMoonsVisited?.Invoke(); //Tells the boss spawner to spawn the boss
            }
        }
        //Only for testing!
        if(Input.GetKeyDown(KeyCode.B))
        {
            OnAllMoonsVisited?.Invoke();
        }
    }

    void OpenPortal()
    {
        portalOpen = true;
        sr.enabled = true;
        PortalOpen?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody") && portalOpen)
        {
            PlayerEnteredPortal?.Invoke();
            Debug.Log("Player Won!"); //Her vinner spilleren, transition til Win Screen
        }
    }

    void AddToMoonCounter()
    {
        moonCounter += 1;
    }
}
