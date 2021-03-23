using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [Header("Assign projectile prefabs")]
    [Tooltip("This array should contain ONE instance of every projectile prefab")]
    [SerializeField] private GameObject[] projectileTypes;
    [SerializeField] private int numberOfProjectilesToSpawn = 10;

    private List<GameObject> projectilesType0;
    private List<GameObject> projectilesType1;
    private List<GameObject> projectilesType2;
    private List<GameObject> projectilesType3;
    private GameObject currentProjectileLoaded;
    private int _ID;

    private void Awake()
    {
        _ID = 0;
        projectilesType0 = new List<GameObject>(); //Initialize the list
        projectilesType1 = new List<GameObject>(); //Initialize the list
        projectilesType2 = new List<GameObject>(); //Initialize the list
        projectilesType3 = new List<GameObject>(); //Initialize the list

        InstantiateProjectiles();
    }

    private void InstantiateProjectiles()
    {
        foreach(GameObject _projectilePrefab in projectileTypes) //Runs once for each type of projectile
        {
            GameObject projectilePool = Instantiate(new GameObject("Pool for " + _projectilePrefab)); //Instantiate a new empty gameobject to act as a pool


            print("The ID is: " + _ID);
            //_ID++; //Increment the ID each time the code is run
            switch (_ID)
            {
                case 0:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, projectilePool.transform);
                        projectilesType0.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        _ID = 1;
                    }
                    break;
                case 1:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, projectilePool.transform);
                        projectilesType1.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        _ID = 2;
                    }
                    break;
                case 2:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, projectilePool.transform);
                        projectilesType1.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        _ID = 3;
                    }
                    break;
                case 3:
                    for (int i = 0; i < numberOfProjectilesToSpawn; i++) //Spawns a set number of each projectile
                    {
                        GameObject newInstance = Instantiate(_projectilePrefab, transform.position, Quaternion.identity, projectilePool.transform);
                        projectilesType1.Add(newInstance); //Add the new instance to its own list

                        newInstance.SetActive(false);
                        _ID = 4;
                    }
                    break;
            }

        }
    }

    private GameObject LoadProjectile(int _ID)
    {
        switch(_ID)
        {
            case 0:
                print("Loaded weapon for case: " + 0);
                return currentProjectileLoaded = projectilesType0[0];
            default:
                return null;
        }
    }
}
