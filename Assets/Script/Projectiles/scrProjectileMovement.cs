using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileMovement : MonoBehaviour
{
    [Tooltip("How long the projectile lasts in the scene if it does not hit anything. Defaults to 3.5 seconds")]
    [SerializeField] protected float projectileLifeTime = 3.5f;
    [Range(15f, 30f)]
    [SerializeField] protected float movementSpeed = 20f;

    private playerController playerController;
    protected float playerMovementSpeed;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<playerController>(); //Gets the reference
        playerMovementSpeed = 0f;
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
