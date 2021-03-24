using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileMovement : MonoBehaviour
{
    [Tooltip("How long the projectile lasts in the scene if it does not hit anything. Defaults to 3.5 seconds")]
    [SerializeField] private float projectileLifeTime = 3.5f;
    [Range(15f, 30f)]
    [SerializeField] private float movementSpeed = 20f;
    

    private void Update()
    {
        MovePorjectile();
    }
    protected virtual void MovePorjectile() //I made this virtual because it allows us to edit and change the way the other projectiles move
    {
        //print("Parent moving");
        transform.position += (transform.up * movementSpeed * Time.deltaTime); //Makes the projectile move in the direction its facing
    }

    private void DissableProjectileAfterSetTime(float _projectileTimer)
    {
        print("Function for starting iEnumerator called");
        StartCoroutine(StartProjectileTimer(_projectileTimer));
    }
    private IEnumerator StartProjectileTimer(float _projectileTimer)
    {
        print("iEnumerator called");
        yield return new WaitForSeconds(_projectileTimer);
        this.gameObject.SetActive(false); //Dissables this gameobject
    }
    private void FireProjectile(GameObject _projectile)
    {
        if(this.gameObject == _projectile) //Check that the reference is correct
        {
            print("Projectile fire called");
            DissableProjectileAfterSetTime(projectileLifeTime);
        }
    }
    private void OnEnable()
    {
        scrPlayerProjectileLoader.OnFireWeapon += FireProjectile;
    }
    private void OnDisable()
    {
        scrPlayerProjectileLoader.OnFireWeapon -= FireProjectile;
    }
}
