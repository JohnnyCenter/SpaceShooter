using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileLevelTracker : MonoBehaviour
{
    private scrPlayerProjectileLoader projectileLoader;
    private int projectileLevel;
    public int ProjectileLevel { get; private set; }
    [Tooltip("Sets the placement of this projectileLevelTracker")]
    private int projectilePlacementLeftRight;

    private void Awake()
    {
        projectileLoader = GetComponent<scrPlayerProjectileLoader>(); //Get the instance
        projectileLevel = 0;
        ProjectileLevel = projectileLevel;
    }
    private void Start()
    {
        projectilePlacementLeftRight = projectileLoader.UpgradeLeft_RightPlacement; //Do not call this on awake
    }
    public void WeaponPurchased() //Called the first time a weapon is purchased
    {
        ProjectileLevel += 1;
        print("Projectile upgraded. Current level: " + ProjectileLevel);
        UpdateProjectileStats(ProjectileLevel);
    }
    public void IncreaseProjectileLevel(int _upgradePlacement) //Called whenever the weapon is upgraded
    {
        if(projectilePlacementLeftRight == _upgradePlacement)
        {
            ProjectileLevel += 1;
            print("Projectile upgraded. Current level: " + ProjectileLevel);
            UpdateProjectileStats(ProjectileLevel);
        }
    }
    private void UpdateProjectileStats(int _projectileLevel)
    {
        if (_projectileLevel < 4)
        {
            switch (_projectileLevel) //This is tested to work!
            {
                case 1:
                    //Uppgrade or assign stats
                    projectileLoader.UpdatePorjectileLevel(1);
                    return;
                case 2:
                    //Uppgrade or assign stats
                    projectileLoader.UpdatePorjectileLevel(2);
                    return;
                case 3:
                    //Uppgrade or assign stats
                    projectileLoader.UpdatePorjectileLevel(3);
                    return;
                case 4:
                    //Uppgrade or assign stats
                    projectileLoader.UpdatePorjectileLevel(4);
                    return;
                default:
                    Debug.LogError("Invalid int value for switch statement for the UpdateProjectileStats method in the scrProjectileLevelTracker class");
                    return;
            }
        }
        else
            return;
    }
}
