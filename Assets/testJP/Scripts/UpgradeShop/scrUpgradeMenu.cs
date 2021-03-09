using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a singleton, do not place this class on any objects with the exception of the "upgradePanelManager".
/// </summary>
public class scrUpgradeMenu : MonoBehaviour
{
    private static scrUpgradeMenu instance; //Make this upgrade menu into a singleton

    public static scrUpgradeMenu Instance { get { return instance; }}

    [SerializeField] private GameObject upgradePanel; //Don`t forget to set this in the inspector!
    [SerializeField] private GameObject upgradePlacementPanel; //Don`t forget to set this in the inspector!
    public GameObject UpgradePLacementPanel { get; private set; }
    private void Awake()
    {
        UpgradePLacementPanel = upgradePlacementPanel;
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
        upgradePanel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UpgradePLacementPanel.SetActive(true);
        }
    }
    public void OpenUpgradeMenuWithPlacementReference(int placement)
    {
        upgradePanel.SetActive(true);
        print("Upgrade menu is opened with the following placement: " + placement);
        /*switch(placement)
        {

        }*/
    }
    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        //upgradePlacementPanel.SetActive(false); //Call this here?
    }
}
