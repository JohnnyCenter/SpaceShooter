using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Upgrades")]
public class UpgradesSO : ScriptableObject
{
    [Tooltip("Set the upgrade-prefab that corresponds to this upgrade")]
    public GameObject upgradePrefab;
    public GameObject ActualWeaponInstantiated { get; private set; }
    [HideInInspector] public scrUpgradeWeapon weaponUpgrader;
    [Tooltip("Set the cost of this upgrade")]
    [SerializeField] private int upgradeCost;
    [Tooltip("Set the cost of upgrading this upgrade")]
    [SerializeField] private int upgradingCost;
    public int UpgradingCost { get; set; }
    public int UpgradeCost { get; private set; }
    [Tooltip("Set the name of the upgrade")]
    [SerializeField] private string upgradeName;
    private Vector3 upgradeLocationLeft;
    private Vector3 upgradeLocationRight;
    private Quaternion upgradeRotation;
    private float playerWidth = 1.4f;
    [Tooltip("Assign projectile type by number. The projectile will be the same as the INDEX NUMBER of the projectile type in the array in the scrPlayerProjectileLoader class")]
    [SerializeField] private int projectileType;
    public int ProjectileType { get; private set; }

    private GameObject playerBody;
    public string UpgradeName { get; private set; }
    public void ResetUpgradeStats()
    {
        ProjectileType = projectileType;
        playerBody = GameObject.FindGameObjectWithTag("PlayerBody");
        UpgradeCost = upgradeCost;
        UpgradingCost = upgradingCost;
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
    public Vector3 SetUpgradeLocationLeft(Vector3 _playerPossition)
    {
        upgradeLocationLeft = _playerPossition;
        upgradeLocationLeft.x = _playerPossition.x - playerWidth;
        upgradeLocationLeft.z = _playerPossition.z + 31.26f;
        return upgradeLocationLeft;
    }
    public Vector3 GetUPgradeLocationLeft()
    {
        return upgradeLocationLeft;
    }
    public Vector3 SetUpgradeLocationRight(Vector3 _playerPossition)
    {
        upgradeLocationRight = _playerPossition;
        upgradeLocationRight.x = _playerPossition.x + playerWidth;
        upgradeLocationRight.z = _playerPossition.z + 31.26f;
        return upgradeLocationRight;
    }
    public Quaternion SetUpgradeRotation(Vector3 _playerRotation)
    {
        upgradeRotation.x = 0;
        return upgradeRotation;
    }
    public Vector3 GetUpgradeLocationRight()
    {
        return upgradeLocationRight;
    }

    public void UpgradePurchased(int placement)
    {
        switch(placement)
        {
            case 0:
                ActualWeaponInstantiated = Instantiate(upgradePrefab, SetUpgradeLocationLeft(playerBody.transform.position), new Quaternion(-90f, 0f, 0f, 90), playerBody.transform); //Instantiate the upgrade with the player as parent
                return;
            case 1:
                ActualWeaponInstantiated = Instantiate(upgradePrefab, SetUpgradeLocationRight(playerBody.transform.position), new Quaternion(-90f, 0f, 0f, 90), playerBody.transform); //Instantiate the upgrade with the player as parent
                return;
            default:
                Debug.LogError("The placement value is incorrect when used for switch statement in the class UpgradeSO");
                return;
        }
    }
}
