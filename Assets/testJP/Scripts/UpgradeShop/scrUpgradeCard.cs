using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrUpgradeCard : MonoBehaviour
{
    [Tooltip("Needs to be assigned the part of the upgrade button that will be switched out for the image of the upgrade. This should be the Upgrade - Image")]
    [SerializeField] private Image UpgradeImage;
    [Tooltip("Needs to be assigned the part of the upgrade button that will be switched out for the upgrade cost text. This should be the UpgradeCost - TMP")]
    [SerializeField] private TextMeshProUGUI UpgradeCost;

    public void SetupUpgradeButton(UpgradeSettingsSO upgradeSettings)
    {
        UpgradeImage.sprite = upgradeSettings.UppgradeSprite; //Gets the sprite from the assigned upgradeSO
        UpgradeCost.text = upgradeSettings.UpgradeCost.ToString(); //Gets the integer cost of the assigned uppgradeSO and converts it into text
    }
}
