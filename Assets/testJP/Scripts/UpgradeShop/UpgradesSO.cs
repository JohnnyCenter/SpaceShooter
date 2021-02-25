using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades")]
public class UpgradesSO : ScriptableObject
{
    [Tooltip("Set the upgrade-prefab that corresponds to this upgrade")]
    public GameObject upgradePrefab;
    [Tooltip("Set the cost of this upgrade")]
    [SerializeField] private int upgradeCost;

    public void UpgradePurchased()
    {
        //This is not final, only works temporarily.
        Instantiate(upgradePrefab);
    }
}
