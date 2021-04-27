using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrEnemyStats : MonoBehaviour
{
    public Action OnEnemyHit;

    public static Action<int> OnEnemyKilled;
    [SerializeField] private EnemyStatsSO stats;
    private int health;
    private bool canTakeDamage;
    private bool canTakeDamageCountdownStarted;
    private bool countDownStarted;
    public bool IsVisibleOnScreen { get; set; }  //Used to affect spawner
    public EnemyStatsSO LocalStats { get; private set; } //For reference in other scripts

    private void Awake()
    {
        stats.resetStats(); //Makes sure any changes to the variables in the scriptableObject is updated before start
    }
    private void Start()
    {
        LocalStats = stats;
        health = stats.Health;
        canTakeDamage = false;
        canTakeDamageCountdownStarted = false;
        countDownStarted = false;
    }
    private void Update()
    {
        if(canTakeDamageCountdownStarted && countDownStarted == false)
        {
            countDownStarted = true;
            StartCoroutine(StartCanTakeDamageCountdown(0.3f));
        }
    }
    private IEnumerator StartCanTakeDamageCountdown(float _timeToWait)
    {
        float timer = 0f;
        timer += Time.deltaTime;
        yield return new WaitForSeconds(_timeToWait);
        canTakeDamage = true;
    }
    public void TakeDamage(int _damage)
    {
        if(canTakeDamage)
        {
            OnEnemyHit?.Invoke();
            print("Ouch, took this much damage: " + _damage);
            health -= _damage;
            if (health <= 0)
            {
                EnemyDies();
            }
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
        canTakeDamage = false;
        canTakeDamageCountdownStarted = false;
        countDownStarted = false;
    }
    public void EnemyDiesNoReward()
    {
        //Enemy dies
        gameObject.SetActive(false);
        IsVisibleOnScreen = false;
        health = stats.Health;
        canTakeDamage = false;
        canTakeDamageCountdownStarted = false;
        countDownStarted = false;
    }
    private void OnEnable()
    {
        health = stats.Health;
        canTakeDamage = false;
        canTakeDamageCountdownStarted = false;
        countDownStarted = false;
    }
    private void OnBecameVisible()
    {
        health = stats.Health;
        canTakeDamageCountdownStarted = true;
        IsVisibleOnScreen = true;
    }
    private void OnBecameInvisible()
    {
        IsVisibleOnScreen = false;
    }
}
