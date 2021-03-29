using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileLevel : MonoBehaviour
{
    [HideInInspector] public ProjectileStatsSO stats;
    [Header("Stats")]
    [Tooltip("Assign the stats for the projectile level 0")]
    [SerializeField] private ProjectileStatsSO statsLevel0;
    [Tooltip("Assign the stats for the projectile level 1")]
    [SerializeField] private ProjectileStatsSO statsLevel1;
    [Tooltip("Assign the stats for the projectile level 2")]
    [SerializeField] private ProjectileStatsSO statsLevel2;
    [Tooltip("Assign the stats for the projectile level 3")]
    [SerializeField] private ProjectileStatsSO statsLevel3;

    private void Start()
    {
        //Reset the stats
        statsLevel0.ResetStats();
        statsLevel1.ResetStats();
        statsLevel2.ResetStats();
        statsLevel3.ResetStats();
    }
    public void UpdateProjectileLevel(int _assignedLevel)
    {
        switch(_assignedLevel) //Change what STATS(SO) is used based on level
        {
            case 0:
                //Use specified SO for stats
                stats = statsLevel0;
                stats.ResetStats();
                return;
            case 1:
                stats = statsLevel1;
                stats.ResetStats();
                return;
            case 2:
                stats = statsLevel2;
                stats.ResetStats();
                return;
            case 3:
                stats = statsLevel3;
                stats.ResetStats();
                return;
            case 4:
                return;
            default:
                Debug.LogError("The value for the int passed to the UpdateProjectileLevel funcion in the scrProjectileLevel class is invalid.");
                return;
        }
    }
}
