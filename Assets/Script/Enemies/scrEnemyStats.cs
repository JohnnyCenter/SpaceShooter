﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrEnemyStats : MonoBehaviour
{
    public Action OnEnemyHit;
    private scrEnemyAttack localAttackScript;
    public static Action<int> OnEnemyKilled;
    [SerializeField] private EnemyStatsSO stats;
    private int health;
    private bool canTakeDamage;
    private bool canTakeDamageCountdownStarted;
    private bool countDownStarted;
    private AudioSource audioPlayer;
    [SerializeField] private AudioClip takeResistedDamageSound;
    [SerializeField] private AudioClip takeWeaknessDamageSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private bool isLakiutuProjectile;
    [Tooltip("How much extra damage is taken when weakness is exploited")]
    [Range(1, 4)]
    [SerializeField] private int weaknessMultiplier;
    public bool IsVisibleOnScreen { get; set; }  //Used to affect spawner
    public EnemyStatsSO LocalStats { get; private set; } //For reference in other scripts
    private scrEnemyWeaknessTracker weaknessTracker;
    private void Awake()
    {
        stats.resetStats(); //Makes sure any changes to the variables in the scriptableObject is updated before start
        audioPlayer = GetComponent<AudioSource>(); //Get the local reference
        TryGetComponent<scrEnemyAttack>(out localAttackScript);
    }
    private void Start()
    {
        weaknessTracker = GetComponent<scrEnemyWeaknessTracker>(); //Gets the instance
        LocalStats = stats;
        health = stats.Health;
        if(!isLakiutuProjectile)
        {
            canTakeDamage = false;
            canTakeDamageCountdownStarted = false;
            countDownStarted = false;
        }

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
    public void TakeDamage(int _damage, int _damageType)
    {
        if(_damageType == weaknessTracker.WeaknessIndex || weaknessTracker.WeaknessIndex == 4)
        {
            print("Weakness!");
            //Special extra damage effect
            audioPlayer.clip = takeWeaknessDamageSound;
            _damage = (_damage * weaknessMultiplier);
        }
        if(isLakiutuProjectile)
        {
            Destroy(this.gameObject);
        }
        if(canTakeDamage)
        {
            OnEnemyHit?.Invoke();
            print("Ouch, took this much damage: " + _damage);
            health -= _damage;
            if(_damageType != weaknessTracker.WeaknessIndex || weaknessTracker.WeaknessIndex != 4)
            {
                audioPlayer.clip = takeResistedDamageSound;
            }
            audioPlayer.Play();

            if (health <= 0)
            {
                EnemyDies();
            }
        }
    }
    private void EnemyDies()
    {
        //Dissable enemy shooting
        if(localAttackScript != null)
        {
            localAttackScript.IsDead = true; //Prevents the enemy from fiering
        }

        //Add to player scrap
        audioPlayer.clip = deathSound;
        audioPlayer.Play();
        OnEnemyKilled?.Invoke(stats.ScrapReward);
        StartCoroutine(DeathWaitForSound(1f));
        canTakeDamage = false;
        canTakeDamageCountdownStarted = false;
        countDownStarted = false;
    }
    private IEnumerator DeathWaitForSound(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        //Enemy dies
        gameObject.SetActive(false);
        IsVisibleOnScreen = false;
        health = stats.Health;
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
    private void DeleteThisOnPLayerDeath()
    {
        Destroy(this.gameObject);
    }
    private void OnEnable()
    {
        scrPlayerHealth.OnPlayerDeath += DeleteThisOnPLayerDeath;
        health = stats.Health;
        canTakeDamage = false;
        canTakeDamageCountdownStarted = false;
        countDownStarted = false;
    }
    private void OnDisable()
    {
        scrPlayerHealth.OnPlayerDeath -= DeleteThisOnPLayerDeath;
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
