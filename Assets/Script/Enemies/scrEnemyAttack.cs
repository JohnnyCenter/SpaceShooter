using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyAttack : MonoBehaviour
{
    private scrEnemyMovement enemyMovement;
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private float timeBetweenAttacks;
    private float weaponCooldownTimer;
    private bool canFire;
    Quaternion projectileRotation;

    private void Awake()
    {
        enemyMovement = GetComponent<scrEnemyMovement>();
    }
    private void Start()
    {
        weaponCooldownTimer = 0f;
        canFire = true;
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
    private void OnBecameVisible()
    {
        projectileRotation = (transform.rotation);
    }
}
