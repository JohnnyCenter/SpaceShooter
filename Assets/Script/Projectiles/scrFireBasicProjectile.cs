using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrFireBasicProjectile : MonoBehaviour
{
    [Tooltip("The projectile that the player starts with")]
    [SerializeField] private GameObject basicProjectile;
    [SerializeField] private GameObject basicWeaponFirePos;
    [Tooltip("Set the fire rate for the basic weapon")]
    [SerializeField] private float basicWeaponFireRate;
    private List<GameObject> basicProjectiles;
    private bool canFire;
    private GameObject currentlyLoadedProjectile;

    public static event Action<GameObject> OnFireBasicWeapon;


    [SerializeField]
    playerController pc;
    Quaternion playerRotation;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody"); //Get the player instance
        basicProjectiles = new List<GameObject>(); //Initialize the list
        InstantiateBasicProjectiles();
        pc = FindObjectOfType<playerController>(); //TEMPORARY CHANGE TO CONNECT PLAYERCONTROLLER WITH THIS SCRIPT//
    }
    private void Start()
    {
        canFire = true;
        LoadProjectile();
    }
    private void Update()
    {
        playerRotation = player.transform.rotation; //Update the player rotation, so that projectiles are facing the right direction when the player turns
        if (Input.GetKeyDown(KeyCode.Space) || pc.firing == true) //ADDED PC.FIRING == TRUE//
        {
            //A function for loading the basic projectile
            if(canFire)
            {
                FireBasicWeapon(LoadProjectile());
            }
        }
    }
    private void InstantiateBasicProjectiles()
    {
        GameObject projectilePool = Instantiate(new GameObject("Pool for " + basicProjectile)); //Instantiate a new empty gameobject to act as a pool
        for (int i = 0; i < 20; i++)
        {
            GameObject newinstance = Instantiate(basicProjectile, transform.position, playerRotation, projectilePool.transform); //Creates a new instance, setting the parent to a new empty object
            basicProjectiles.Add(newinstance); //Adds the new instance to the list
            newinstance.SetActive(false);
        }
    }
    private GameObject LoadProjectile()
    {
        for (int i = 0; i < basicProjectiles.Count; i++)
        {
            if (!basicProjectiles[i].activeInHierarchy)
            {
                print("Found a projectile!");
                return currentlyLoadedProjectile = basicProjectiles[i];
            }
        }
        return null;
    }
    private void FireBasicWeapon(GameObject basicProjectile) //Fires the main basic weapon
    {
        if(basicProjectile == null)
        {
            return;
        }
        //Set possition and align projectile
        basicProjectile.transform.position = basicWeaponFirePos.transform.position;
        basicProjectile.transform.rotation = playerRotation;
        //Update projectile stats based on weapon level
        //scrProjectileLevel loadedProjectileLevel = basicProjectile.GetComponent<scrProjectileLevel>();
        //loadedProjectileLevel.UpdateProjectileLevel(projectileLevel); //Updates the stats for the loaded projectile 
        //Set the projectile to active and fire it
        basicProjectile.SetActive(true);
        OnFireBasicWeapon?.Invoke(basicProjectile);
        canFire = false;
        float weaponCooldown = basicWeaponFireRate; //Gets the fire rate from the current loaded projectile
        StartCoroutine(WeaponCooldown(weaponCooldown)); //Sets cooldown length
    }
    private IEnumerator WeaponCooldown(float timer)
    {
        yield return new WaitForSeconds(timer);
        canFire = true;
    }
}
