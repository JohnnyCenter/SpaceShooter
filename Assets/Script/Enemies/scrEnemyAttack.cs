using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyAttack : MonoBehaviour
{
    private GameObject thePlayer;
    private scrEnemyMovement enemyMovement;
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private float timeBetweenAttacks;
    [Tooltip("Decides if the enemy has a specific possition from where it fires projectiles.")]
    [SerializeField] private bool hasFirePossition;
    [Tooltip("Decides what possition this projectile fires from, 0 = left. 1 = right")]
    [Range(0,1)]
    [SerializeField] private int firePossition;
    [SerializeField] private Transform FirePossitionA;
    [SerializeField] private Transform FirePossitionB;
    [SerializeField] private bool canUseFranticAttack;
    private scrEnemyStats enemyStats;
    private float weaponCooldownTimer;
    private bool canFire;
    public bool IsDead { get; set; }
    Quaternion projectileRotation;
    private float franticAttackTimer;
    private float timeSinceFranticAttack;
    public bool FranticAttack { get; private set; }
    private bool playerIsDead;

    private AudioSource SoundSource;

    //Sabotage related
    private bool sabotaged;
    private float timeSinceSabotaged;
    [Tooltip("How many seconds the enemy is sabotaged")]
    [SerializeField] private float sabotageTime = 5f;


    private void Awake()
    {
        IsDead = false;
        enemyMovement = GetComponent<scrEnemyMovement>();
        enemyStats = GetComponent<scrEnemyStats>();
        SoundSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        playerIsDead = false;
        weaponCooldownTimer = 0f;
        canFire = false;
        franticAttackTimer = 1.5f;
        FranticAttack = false;
    }
    private void Update()
    {
        if (sabotaged)
        {
            timeSinceSabotaged += Time.deltaTime;
            if (timeSinceSabotaged >= sabotageTime)
            {
                timeSinceSabotaged = 0;
                sabotaged = false;
            }
        }
        if (playerIsDead)
        {
            return;
        }
        if(enemyMovement.IAmActive && !FranticAttack && !IsDead && !playerIsDead && !sabotaged)
        {
            FireBasicProjectile();
        }
        else if(enemyMovement.IAmActive && FranticAttack && timeSinceFranticAttack <= franticAttackTimer && canUseFranticAttack && !IsDead && !playerIsDead && !sabotaged)
        {
            timeSinceFranticAttack += Time.deltaTime;
            if (timeSinceFranticAttack >= franticAttackTimer)
            {
                FranticAttack = false;
            }
            FireFranticAttack();
        }
    }
    public void SetFranticAttack()
    {
        if(canUseFranticAttack)
        {
            timeSinceFranticAttack = 0f;
            FranticAttack = true;
        }
    }
    private void FireFranticAttack()
    {
        //Calculate frantic aim

        weaponCooldownTimer += (Time.deltaTime * 10);
        if (weaponCooldownTimer >= timeBetweenAttacks)
        {
            canFire = true;
            weaponCooldownTimer = 0f;
        }
        if (canFire)
        {
            if (!hasFirePossition)
            {
                Instantiate(enemyProjectile, transform.position, projectileRotation);
                SoundSource.Play();
                canFire = false;
            }
            else if (hasFirePossition)
            {
                switch (firePossition)
                {
                    case 0:
                        Instantiate(enemyProjectile, FirePossitionA.position, projectileRotation);
                        SoundSource.Play();
                        canFire = false;
                        return;
                    case 1:
                        Instantiate(enemyProjectile, FirePossitionB.position, projectileRotation);
                        SoundSource.Play();
                        canFire = false;
                        return;
                }
            }

        }
    }
    private void FireBasicProjectile()
    {
        weaponCooldownTimer += Time.deltaTime;
        if (weaponCooldownTimer >= timeBetweenAttacks)
        {
            canFire = true;
            weaponCooldownTimer = 0f;
        }
        if (canFire)
        {
            if(!hasFirePossition)
            {
                Instantiate(enemyProjectile, transform.position, projectileRotation);
                SoundSource.Play();
                canFire = false;
            }
            else if (hasFirePossition)
            {
                switch(firePossition)
                {
                    case 0:
                        Instantiate(enemyProjectile, FirePossitionA.position, projectileRotation);
                        SoundSource.Play();
                        canFire = false;
                        return;
                    case 1:
                        Instantiate(enemyProjectile, FirePossitionB.position, projectileRotation);
                        SoundSource.Play();
                        canFire = false;
                        return;
                }
            }

        }
    }
    private void RotateEnemyProjectile(Quaternion _newRotation)
    {
        //print("Rotating enemies...");
        projectileRotation = _newRotation;
    }
    private void dissableEnemyAttack()
    {
        playerIsDead = true;
    }
    private void IsSabotaged()
    {
        sabotaged = true;
        timeSinceSabotaged = 0f;
    }
    private void OnEnable()
    {
        scrSabotage.OnSabotageTriggered += IsSabotaged;
        scrPlayerHealth.OnPlayerDeath += dissableEnemyAttack;
        IsDead = false;
        playerController.OnPlayerTurning += RotateEnemyProjectile;
        canFire = false;
        weaponCooldownTimer = 0f;
        FranticAttack = false;
        enemyStats.OnEnemyHit += SetFranticAttack;
    }

    private void OnDisable()
    {
        scrSabotage.OnSabotageTriggered -= IsSabotaged;
        scrPlayerHealth.OnPlayerDeath -= dissableEnemyAttack;
        playerController.OnPlayerTurning -= RotateEnemyProjectile;
        FranticAttack = false;
        enemyStats.OnEnemyHit -= SetFranticAttack;
    }
    private void OnBecameVisible()
    {
        enemyStats.IsVisibleOnScreen = true;
        FranticAttack = false;
        projectileRotation = (transform.rotation);
    }
}
