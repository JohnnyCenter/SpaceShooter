using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrEnemyStats : MonoBehaviour
{
    public static Action<int> OnEnemyKilled;
    [SerializeField] private EnemyStatsSO stats;
    private int health;
    public bool IsVisibleOnScreen { get; set; }

    private void Awake()
    {
        stats.resetStats(); //Makes sure any changes to the variables in the scriptableObject is updated before start
    }
    private void Start()
    {
        health = stats.Health;
    }
    public void TakeDamage(int _damage)
    {
        print("Ouch, took this much damage: " + _damage);
        health -= _damage;
        if(health <= 0)
        {
            EnemyDies();
        }
    }
    private void EnemyDies()
    {
        //Add to player scrap
        OnEnemyKilled?.Invoke(stats.ScrapReward);
        //Enemy dies
        gameObject.SetActive(false);
        IsVisibleOnScreen = false;
        health = stats.Health;
    }
}
