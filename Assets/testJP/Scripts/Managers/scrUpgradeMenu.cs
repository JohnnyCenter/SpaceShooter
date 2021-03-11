using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// This is a singleton, do not place this class on any objects with the exception of the "upgradePanelManager".
/// </summary>
public class scrUpgradeMenu : MonoBehaviour
{
    private static scrUpgradeMenu instance; //Make this upgrade menu into a singleton

    public static scrUpgradeMenu Instance { get { return instance; }}

     [SerializeField] private GameObject upgradePanel; //Don`t forget to set this in the inspector!
     [SerializeField] private GameObject upgradePlacementPanel; //Don`t forget to set this in the inspector!
     [SerializeField] private GameObject purchasePanel; //Don`t forget to set this in the inspector!
     [SerializeField] private GameObject upgradeTheUpgradePanel; //Don`t forget to set this in the inspector!
    public GameObject UpgradePLacementPanel { get; private set; }
    //public int UpgradePlacement { get; private set; } //Buttons rerference this variable for upgrade placement
    public static Action<int> OnPlacementSelected;

    private void Awake()
    {
        UpgradePLacementPanel = upgradePlacementPanel;
        //UpgradePlacement = 0; //Assign a default value

        if(instance != null && instance != this)
        {
            Destroy(this.gameObject); //We do not want duplicates of this class
        }
        else
        {
            instance = this;
        }
        if(upgradePanel == null || upgradePlacementPanel == null) //Make sure these are assigned!
        {
            Debug.LogError("upgradePanel and/or upgradePlacementPanel is not assigned! Drag those UI panels to the corresponding fields in the UpgradePanelManager found in the inspector!");
        }
    }

    private void Start()
    {
        purchasePanel.SetActive(false);
        upgradePanel.SetActive(false);
        UpgradePLacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            purchasePanel.SetActive(true);
        }
    }
    public void OpenUpgradeTheUpgradePanel()
    {
        upgradeTheUpgradePanel.SetActive(true);
        purchasePanel.SetActive(false);
    }
    public void OpenUpgradePlacementPanel()
    {
        purchasePanel.SetActive(false);
        UpgradePLacementPanel.SetActive(true);
    }
    public void OpenUpgradeMenuWithPlacementReference(int placement)
    {
        upgradePanel.SetActive(true);
        //print("Upgrade menu is opened with the following placement: " + placement);
        //UpgradePlacement = placement;
        OnPlacementSelected?.Invoke(placement);
    }
    public void CloseUpgradePanel()
    {
        purchasePanel.SetActive(false);
        upgradePanel.SetActive(false);
        upgradePlacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
    }
}
