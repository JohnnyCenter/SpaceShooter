using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrUpgradeManagerUI : MonoBehaviour
{
    private GameObject upgradePanel;
    [SerializeField] private GameObject upgradeCardPrefab;
    [SerializeField] private Transform upgradePanelContainer;

    [SerializeField] private UpgradeSettingsSO[] upgrades;
    //How will this work??

    //A: Player reaches upgrade station (or button press for now) - > Open upgrade window

    //B: Upgrade window contains several upgrade prefabs with text description and UI image icon. These are based off of scriptable objects

    //C: When a player clicks on one of those upgrades, it will activate a button that will then send a message to a upgrade function

    //D: Upgrade function checks if the player has enough "scrap", if yes -> Upgrade is purchased. -> Upgrade becomes "greyed out" in the upgrade manager

    //E: Player prefab is updated with the new upgrade. -> New/improved player functionality -> Sets the corresponding part of the player prefab to active.

    private void Awake()
    {
        upgradePanel = GameObject.Find("UpgradePanel");
        upgradePanel.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < upgrades.Length; i++) //Create the upgrade buttons at game start
        {
            CreateUpgradeCard(upgrades[i]);
        }
    }

    private void CreateUpgradeCard(UpgradeSettingsSO upgrade) //Create buttons for upgradeSO
    {
        GameObject newinstance = Instantiate(upgradeCardPrefab, upgradePanelContainer.position, Quaternion.identity); //Instantiate it at panelContainer location
        newinstance.transform.SetParent(upgradePanelContainer); //Parent it to the panelContainer
        newinstance.transform.localScale = Vector3.one; //To avoid potential scaling errors when using a grid layout

        scrUpgradeCard cardButton = newinstance.GetComponent<scrUpgradeCard>(); //Assigns the newInstance vars to a "cardButton" and gives it the scrUpgradeCard class
        cardButton.SetupUpgradeButton(upgrade);
    }

    public void PurchaseUpgrade()
    {
        //Check if player has enough scrap

        //Get instance of upgrade from button pressed

        //Set the upgrade instance to active
    }
    public void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true); //"Opens" the upgrade panel
    }
    public void CLoseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
}
