using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// This script should sit on two different gameobjects placed at the weapon placement
/// </summary>
public class scrPlayerProjectileLoader : MonoBehaviour
{
    //Initialization:
    //instantiate 10 projectiles for each possible weapon type at awake
    //place these into lists, and parent them to containers

    //Loading the correct projectile:
    //use an int for "ID" to load the right porjectile whenever the player fires
    //movement logic handled on each projectile
    //Use a switch statement for the ID. 

    //Sorting weapons with ID: 
    //Have a reference to a "playerCurrentWeapon", that stores the ID of what weapon we are using.
    //Update a local ID var using that ID

    //Using a level var for projectiles:
    //As the weapon is upgraded, an upgrade event is called that upgrades the stats on the weapons with the level!
    private scrProjectileLevelTracker projectileLevelTracker;

    [Header("Assign projectile prefabs")]
    [Tooltip("This array should contain ONE instance of every projectile prefab")]
    [SerializeField] private GameObject[] projectileTypes;
    [SerializeField] private int numberOfProjectilesToSpawn = 10;
    [Header("Possition")]
    [Tooltip("0 = left possition. 1 = right possition.")]
    [Range(0, 1)]
    [SerializeField] private int upgradeLeft_RightPlacement = 0;
    public int UpgradeLeft_RightPlacement { get; private set; }
    private int projectileLevel;

    private List<GameObject> projectilesType0;
    private List<GameObject> projectilesType1;
    private List<GameObject> projectilesType2;
    private List<GameObject> projectilesType3;
    private GameObject currentProjectileLoaded;
    private int projectileID;
    public int CurrentWeaponID { get; private set; } //Needs to be set when wheapon is purchased
    public static event Action<GameObject> OnFireWeapon;
    private bool weaponCanFire;

    [Header("Assign fire possitions")]
    [SerializeField] private GameObject firePossitionLeft;
    [SerializeField] private GameObject firePossitionRight;
    private Vector3 firePossition;
    Quaternion playerRotation;
    private GameObject player;


    [SerializeField]
    private AudioSource SoundSource;
    private AudioClip Sound;

    [SerializeField]
    playerController pc; //TEMPORARY CHANGE TO CONNECT PLAYERCONTROLLER WITH THIS SCRIPT//

    private void Awake()
    {
        weaponCanFire = true;
        projectileLevel = 0;
        projectileLevelTracker = GetComponent<scrProjectileLevelTracker>(); //Gets the instance of the level tracker
        player = GameObject.FindGameObjectWithTag("PlayerBody"); //Get the player instance
        CurrentWeaponID = -1; //No weapon is purchased
        projectileID = 0;
        projectilesType0 = new List<GameObject>(); //Initialize the list
        projectilesType1 = new List<GameObject>(); //Initialize the list
        projectilesType2 = new List<GameObject>(); //Initialize the list
        projectilesType3 = new List<GameObject>(); //Initialize the list

        UpgradeLeft_RightPlacement = upgradeLeft_RightPlacement;
        InstantiateProjectiles();
        pc = FindObjectOfType<playerController>(); //TEMPORARY CHANGE TO CONNECT PLAYERCONTROLLER WITH THIS SCRIPT//
        SoundSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (upgradeLeft_RightPlacement == 0)
        {
            firePossition = firePossitionLeft.transform.position; //Decides the possition the porjectiles spawn at.
        }
        else if (upgradeLeft_RightPlacement == 1)
        {
            firePossition = firePossitionRight.transform.position; //Decides the possition the porjectiles spawn at.
        }
        playerRotation = player.transform.rotation; //Update the player rotation, so that projectiles are facing the right direction when the player turns
        if(Input.GetKeyDown(KeyCode.Space) || pc.firing == true) //ADDED PC.FIRING == TRUE//
        {
            if(CurrentWeaponID == -1) //Check that a weapon is purchased
            {
                return;
            }
            //print("Weapon is fired");
            LoadProjectile(CurrentWeaponID);
            FireProjectile(currentProjectileLoaded);
        }
    }
    private void InstantiateProjectiles()
    {
        foreach(GameObject _projectilePrefab in projectileTypes) //Runs once for each type of projectile
        {
            GameObject projectilePool = Instantiate(new GameObject("Pool for " + _projectilePrefab)); //Instantiate a new empty gameobject to act as a pool


            //print("The ID is: " + projectileID);
            //_ID++; //Increment the ID each time the code is run
            switch (projectileID)
            {
                case 0:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, playerRotation, projectilePool.transform);
                        projectilesType0.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        projectileID = 1;
                    }
                    break;
                case 1:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, playerRotation, projectilePool.transform);
                        projectilesType1.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        projectileID = 2;
                    }
                    break;
                case 2:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, playerRotation, projectilePool.transform);
                        projectilesType2.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        projectileID = 3;
                    }
                    break;
                case 3:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, playerRotation, projectilePool.transform);
                        projectilesType3.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        projectileID = 4;
                    }
                    break;
            }

        }
    }
    private GameObject LoadProjectile(int weaponType) //The int it is assigned is "CurrentWeaponID"
    {
        switch(weaponType)
        {
            case 0:
                //print("Loaded projectile type: " + 0);
                return currentProjectileLoaded = GetInstanceFromPool(projectilesType0);
            case 1:
                //print("Loaded projectile type: " + 1);
                return currentProjectileLoaded = GetInstanceFromPool(projectilesType1);
            case 2:
                //print("Loaded projectile type: " + 2);
                return currentProjectileLoaded = GetInstanceFromPool(projectilesType2);
            case 3:
                //print("Loaded projectile type: " + 3);
                return currentProjectileLoaded = GetInstanceFromPool(projectilesType3);
            default:
                Debug.LogError("The value of the int passed to the LoadProjectile function in the scrPlayerProjectileLoader class is incorrect");
                return null;
        }
    }
    private GameObject GetInstanceFromPool(List<GameObject> _list)
    {
        for(int i = 0; i < _list.Count; i ++)
        {
            if(!_list[i].activeInHierarchy)
            {
                return _list[i];
            }
        }
        Debug.Log("There are no more unactive projectiles left in pool. Returning null");
        return null;
    }
    private void FireProjectile(GameObject _loadedProjectile)
    {
        if(_loadedProjectile == null || weaponCanFire == false)
        {
            return;
        }

        
        //Set possition and align projectile
        _loadedProjectile.transform.position = firePossition;
        _loadedProjectile.transform.rotation = playerRotation;
        //Update projectile stats based on weapon level
        scrProjectileLevel loadedProjectileLevel = _loadedProjectile.GetComponent<scrProjectileLevel>();
        loadedProjectileLevel.UpdateProjectileLevel(projectileLevel); //Updates the stats for the loaded projectile
        //Add recoil by level

        //LoadSound
        Sound = loadedProjectileLevel.stats.BulletSound;
        SoundSource.clip = Sound;
        SoundSource.Play();
        //print("LoadedSoundIs: "+ Sound);
        //Set the projectile to active and fire it
        _loadedProjectile.SetActive(true);
        OnFireWeapon?.Invoke(_loadedProjectile);
        weaponCanFire = false;
        float weaponCooldown = loadedProjectileLevel.stats.WeaponFireRate; //Gets the fire rate from the current loaded projectile
        StartCoroutine(WeaponCooldown(weaponCooldown)); //Sets cooldown length
    }
    private IEnumerator WeaponCooldown(float timer)
    {
        weaponCanFire = false;
        yield return new WaitForSeconds(timer);
        weaponCanFire = true;
    }
    public void WeaponPurchased(int _weaponType, int placement)
    {
        if(upgradeLeft_RightPlacement == placement)
        {
            print("Weapon purchased");
            CurrentWeaponID = _weaponType;
            projectileLevelTracker.WeaponPurchased();
        }
    }
    public void UpdatePorjectileLevel(int _newLevel) //Update the projectile level
    {
        projectileLevel = _newLevel;
    }
    private void OnEnable()
    {
        StartCoroutine(WeaponCooldown(0.5f));
        scrUpgradeButton.OnWeaponPurchased += WeaponPurchased;
    }
    private void OnDisable()
    {
        scrUpgradeButton.OnWeaponPurchased -= WeaponPurchased;
    }
}
