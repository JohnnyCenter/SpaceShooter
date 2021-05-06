using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileMovement : MonoBehaviour
{
    [Tooltip("How long the projectile lasts in the scene if it does not hit anything. Defaults to 3.5 seconds")]
    [SerializeField] protected float projectileLifeTime = 3.5f;
    [Range(15f, 30f)]
    [SerializeField] protected float movementSpeed = 20f;
    [Tooltip("Decides if the projectile will despawn when it hits target or not")]
    [SerializeField] protected bool isLazer;
    protected scrProjectileLevel projectileStats;
    protected playerController playerController;
    protected float playerMovementSpeed;
    private scrPlayerProjectileType projectileType;

    protected virtual void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<playerController>(); //Gets the reference
        playerMovementSpeed = 0f;
        projectileStats = GetComponent<scrProjectileLevel>(); //Gets the reference
        projectileType = GetComponent<scrPlayerProjectileType>();
    }
    protected void Update()
    {
        if(playerController!= null)
        {
            playerMovementSpeed = playerController.moveSpeed;
        }
        MovePorjectile();
    }
    protected virtual void MovePorjectile() //I made this virtual because it allows us to edit and change the way the other projectiles move
    {
        //print("Parent moving");
        transform.position += (transform.up * (movementSpeed + playerMovementSpeed) * Time.deltaTime); //Makes the projectile move in the direction its facing
    }
    protected virtual void BasicProjectileHit(string _hitEnemy)
    {
        print("Hit enemy: " + _hitEnemy);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.TryGetComponent<scrEnemyStats>(out scrEnemyStats targetStats);
            if(targetStats != null)
            {
                print("Dealing damage to: " + collision);
                targetStats.TakeDamage(projectileStats.stats.WeaponDamage, projectileType.DamageTypeIndex);
                DestroyProjectile();
            }
            BasicProjectileHit(collision.gameObject.name);
        }

        if (collision.CompareTag("Comet"))
        {
            collision.TryGetComponent<Comet>(out Comet cometHealth);
            if(cometHealth != null)
            {
                cometHealth.TakeDamage(projectileStats.stats.WeaponDamage);
                DestroyProjectile();
            }
            BasicProjectileHit(collision.gameObject.name);
        }
    }
    private void DestroyProjectile()
    {
        if(isLazer)
        {
            return;
        }
        gameObject.SetActive(false);
    }

    protected void DissableProjectileAfterSetTime(float _projectileTimer)
    {
        print("Function for starting iEnumerator called");
        StartCoroutine(StartProjectileTimer(_projectileTimer));
    }
    protected IEnumerator StartProjectileTimer(float _projectileTimer)
    {
        print("iEnumerator called");
        yield return new WaitForSeconds(_projectileTimer);
        this.gameObject.SetActive(false); //Dissables this gameobject
    }
    protected void FireProjectile(GameObject _projectile)
    {
        if(this.gameObject == _projectile) //Check that the reference is correct
        {
            print("Projectile fire called");
            DissableProjectileAfterSetTime(projectileLifeTime);
        }
    }
    protected void OnEnable()
    {
        scrPlayerProjectileLoader.OnFireWeapon += FireProjectile;
        scrFireBasicProjectile.OnFireBasicWeapon += FireProjectile;
    }
    protected void OnDisable()
    {
        scrPlayerProjectileLoader.OnFireWeapon -= FireProjectile;
        scrFireBasicProjectile.OnFireBasicWeapon -= FireProjectile;
    }
}
