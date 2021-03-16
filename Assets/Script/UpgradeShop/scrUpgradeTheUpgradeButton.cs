using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class scrUpgradeTheUpgradeButton : MonoBehaviour
{
    //IMPORTANT REGARDING SCRIPT FUNCTIONALITY:
    //This script is placed on two seperate "upgrade sheets".
    //Which of the two versions of this script is run, is decided by the reference in the "scrUpgradeButton" script, which holds reference to a "left" and "right"
    //version of the "upgrade sheet". 
    private Image UpgradeImage; //Used to display a sketch of the upgrade purchased
    [SerializeField] private TextMeshProUGUI upgradeLevelText; //Used to tell display the CURRENT level of the upgrade
    [SerializeField] private TextMeshProUGUI upgradeCost;
    private int upgradeLevel;
    UpgradesSO localUpgrade; //This will be null, until its assigned by the "UpdateTheUpgrade" function bellow.
    private scrUpgradeMenu upgradeMenu;

    private void Awake()
    {
        print("Upgrade panel has awoken");
        UpgradeImage = GetComponent<Image>();
        upgradeLevel = 0;
        upgradeMenu = FindObjectOfType<scrUpgradeMenu>(); //Get the instance (using a singletonpattern to get the instance)
        upgradeLevelText.text = "Level " + upgradeLevel.ToString();
        upgradeCost.text = "0";
    }
    public void UpdateTheUpgrade(UpgradesSO _upgrade)
    {
        print("UpdatedTheUpgrade");
        upgradeLevel += 1;
        upgradeLevelText.text = "Level " + upgradeLevel.ToString();
        upgradeCost.text = _upgrade.UpgradeCost.ToString();
        localUpgrade = _upgrade;
    }
    public void IncreaseUpgradeLevel() //This is called from a button
    {
        if (upgradeLevel >= 1) //Because this code cannot run if we are not already at least level 1 (or else, the upgrade is not yet assigned or purchased)
        {
            //Implement if check to check that we have enough scrap
            upgradeLevel += 1;
            upgradeLevelText.text = "Level " + upgradeLevel.ToString();
            upgradeCost.text = localUpgrade.UpgradeCost.ToString(); //This local upgrade is assigned by the "UpdateTheUpgrade" function found in this script
            upgradeMenu.CloseUpgradePanel();
        }

    }
}
