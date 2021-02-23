using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public class UpgradeSettingsSO : ScriptableObject
{
    [Tooltip("Set the 3D prefab for this upgrade")]
    public GameObject UpgradePrefab;
    [Tooltip("Set the scrap cost of the upgrade")]
    public int UpgradeCost;
    [Tooltip("Set the sprite of the upgrade that will show up in the upgrade menu")]
    public Sprite UppgradeSprite;
}
