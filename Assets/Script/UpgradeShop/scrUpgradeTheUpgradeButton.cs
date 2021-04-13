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
    [Tooltip("0 = left placement. 1 = right placement")]
    [Range(0, 1)]
    [SerializeField] private int upgradePlacement = 0;
    private GameObject thePlayer;
    scrProjectileLevelTracker[] projectileLevelTrackers;
    scrGameManager gameManager;

    private void Awake()
    {
        gameManager = scrGameManager.instance; //Get the instance
        thePlayer = GameObject.FindGameObjectWithTag("ThePlayer"); //Gets the instance of the player
        //print("Upgrade panel has awoken");
        UpgradeImage = GetComponent<Image>();
        upgradeLevel = 0;
        upgradeMenu = FindObjectOfType<scrUpgradeMenu>(); //Get the instance (using a singletonpattern to get the instance)
        upgradeLevelText.text = "Level " + upgradeLevel.ToString();
        upgradeCost.text = "0";
    }
    private void Start()
    {
        projectileLevelTrackers = thePlayer.GetComponentsInChildren<scrProjectileLevelTracker>(); //Gets the projectileLevelTracjers
    }
    public void UpdateTheUpgrade(UpgradesSO _upgrade)
    {
        //print("UpdatedTheUpgrade");
        upgradeLevel += 1;
        upgradeLevelText.text = "Level " + upgradeLevel.ToString();
        upgradeCost.text = _upgrade.UpgradingCost.ToString();
        localUpgrade = _upgrade;
    }
    public void IncreaseUpgradeLevel() //This is called from a button
    {
        if (upgradeLevel >= 1) //Because this code cannot run if we are not already at least level 1 (or else, the upgrade is not yet assigned or purchased)
        {
            if(gameManager.PlayerScrap < localUpgrade.UpgradingCost)
            {
                print("Did not have enough money to purchase the upgrade");
                upgradeMenu.CloseUpgradePanel();
                return;
            }
            //Implement if check to check that we have enough scrap
            gameManager.SpendScrap(localUpgrade.UpgradingCost);
            upgradeLevel += 1;
            upgradeLevelText.text = "Level " + upgradeLevel.ToString();
            localUpgrade.UpgradingCost += (localUpgrade.UpgradeCost / 2);
            upgradeCost.text = localUpgrade.UpgradingCost.ToString(); //This local upgrade is assigned by the "UpdateTheUpgrade" function found in this script
            foreach(scrProjectileLevelTracker levelTracker in projectileLevelTrackers)
            {
                levelTracker.IncreaseProjectileLevel(upgradePlacement);
            }
            upgradeMenu.CloseUpgradePanel();
        }

    }
}
