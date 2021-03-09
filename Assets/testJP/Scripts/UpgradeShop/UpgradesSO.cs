using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Upgrades")]
public class UpgradesSO : ScriptableObject
{
    [Tooltip("Set the upgrade-prefab that corresponds to this upgrade")]
    public GameObject upgradePrefab;
    [Tooltip("Set the cost of this upgrade")]
    [SerializeField] private int upgradeCost;
    public int UpgradeCost { get; private set; }
    [Tooltip("Set the name of the upgrade")]
    [SerializeField] private string upgradeName;
    [Tooltip("assign the location on the player prefab for this uppgrade to spawn when purchased")]
    [SerializeField] private GameObject upgradeLocation;
    private GameObject playerObject;
    public string UpgradeName { get; private set; }

    public void ResetUpgradeStats()
    {
        playerObject = GameObject.FindGameObjectWithTag("ThePlayer");
        UpgradeCost = upgradeCost;
        if (upgradeName != null)
        {
            UpgradeName = upgradeName;
        }
        else if (upgradeName == null)
        {
            Debug.Log("upgrade name is set to null");
            UpgradeName = "not assigned";
        }
    }
    public void UpgradePurchased()
    {
        Instantiate(upgradePrefab, upgradeLocation.transform.position, Quaternion.identity, playerObject.transform); //Instantiate the upgrade with the player as 
        //the parent prefab
    }
}
