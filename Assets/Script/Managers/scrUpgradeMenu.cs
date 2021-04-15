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

    [Header("Assign the menu button")]
    [Tooltip("Drag the openUpgradeMenu (found in the inspector under UI -> Canvas) into this slot")]
    [SerializeField] private GameObject menuButton;

    [Header("Assign panels from the inspector")]
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject upgradePanel; //Don`t forget to set this in the inspector!
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject upgradePlacementPanel; //Don`t forget to set this in the inspector!
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject purchasePanel; //Don`t forget to set this in the inspector!
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
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
        //Set all pannels to active initialy so that local classes can run their "Awake" method to initialize their vars
        purchasePanel.SetActive(true);
        upgradePanel.SetActive(true);
        UpgradePLacementPanel.SetActive(true);
        upgradeTheUpgradePanel.SetActive(true);
        menuButton.SetActive(false);
    }

    private void Start()
    {
        //Set all panels to unactive, so the menu is "closed" at game start
        purchasePanel.SetActive(false);
        upgradePanel.SetActive(false);
        UpgradePLacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    purchasePanel.SetActive(true);
        //}
    }
    public void OpenUpgradeMenu() //Called from the "Menu" button
    {
        if(purchasePanel.activeSelf == false && upgradePanel.activeSelf == false && UpgradePLacementPanel.activeSelf == false && upgradeTheUpgradePanel.activeSelf == false)
        {
            //Debug.Log("Opens upgrade panel");
            purchasePanel.SetActive(true);
            menuButton.SetActive(false); //Hide the open menu button
            Time.timeScale = 0f;

        }
    }
    public void OpenUpgradeTheUpgradePanel()
    {
        upgradeTheUpgradePanel.SetActive(true);
        purchasePanel.SetActive(false);
        Time.timeScale = 0f;

    }
    public void OpenUpgradePlacementPanel()
    {
        purchasePanel.SetActive(false);
        UpgradePLacementPanel.SetActive(true);
        Time.timeScale = 0f;

    }
    public void OpenUpgradeMenuWithPlacementReference(int placement)
    {
        upgradePanel.SetActive(true);
        //print("Upgrade menu is opened with the following placement: " + placement);
        //UpgradePlacement = placement;
        OnPlacementSelected?.Invoke(placement);
        Time.timeScale = 0f;

    }
    public void CloseUpgradePanel()
    {
        purchasePanel.SetActive(false);
        upgradePanel.SetActive(false);
        upgradePlacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
        menuButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
