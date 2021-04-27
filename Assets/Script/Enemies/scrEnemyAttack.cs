using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyAttack : MonoBehaviour
{
    private scrEnemyMovement enemyMovement;
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private float timeBetweenAttacks;
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
            Instantiate(enemyProjectile, transform.position, projectileRotation);
            canFire = false;
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
