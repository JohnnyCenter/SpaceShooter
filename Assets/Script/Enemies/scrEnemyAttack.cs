using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyAttack : MonoBehaviour
{
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
    private scrEnemyStats enemyStats;
    private float weaponCooldownTimer;
    private bool canFire;
    Quaternion projectileRotation;

    private void Awake()
    {
        enemyMovement = GetComponent<scrEnemyMovement>();
        enemyStats = GetComponent<scrEnemyStats>();
    }
    private void Start()
    {
        weaponCooldownTimer = 0f;
        canFire = false;
    }
    private void Update()
    {
        if(enemyMovement.IAmActive)
        {
            FireBasicProjectile();
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
                canFire = false;
            }
            else if (hasFirePossition)
            {
                switch(firePossition)
                {
                    case 0:
                        Instantiate(enemyProjectile, FirePossitionA.position, projectileRotation);
                        canFire = false;
                        return;
                    case 1:
                        Instantiate(enemyProjectile, FirePossitionB.position, projectileRotation);
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
    private void OnEnable()
    {
        playerController.OnPlayerTurning += RotateEnemyProjectile;
        canFire = false;
        weaponCooldownTimer = 0f;
    }
    private void OnDisable()
    {
        playerController.OnPlayerTurning -= RotateEnemyProjectile;
    }
    private void OnBecameVisible()
    {
        enemyStats.IsVisibleOnScreen = true;
        projectileRotation = (transform.rotation);
    }
}
