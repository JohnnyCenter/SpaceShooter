using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class scrUpgradeButton : MonoBehaviour
{
    private GameObject upgradeButton;
    [Header("Assign upgrade button texts")]
    [Tooltip("Drag the text item on the upgrade button that is supposed to contain the upgradeCost")]
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [Tooltip("Drag the text item on the upgrade button that is supposed to contain the upgradeName")]
    [SerializeField] private TextMeshProUGUI upgradeName;
    private bool upgradeIsSold;
    private Image buttonImage;
    [Header("Define upgrade")]
    [SerializeField] private UpgradesSO upgrade;
    private scrUpgradeMenu upgradeMenu;

    private bool middlePurchased;
    private bool leftPurchased;
    private bool rightPurchased;

    private void Awake()
    {
        upgrade.ResetUpgradeStats();
        buttonImage = GetComponent<Image>();
        upgradeIsSold = false;
        upgradeButton = this.gameObject; //Self-assign as gameobject
        upgradeCost.text = upgrade.UpgradeCost.ToString();
        upgradeName.text = upgrade.UpgradeName;
        upgradeMenu = FindObjectOfType<scrUpgradeMenu>(); //Get the instance (using a singletonpattern to get the instance)
        middlePurchased = false;
        leftPurchased = false;
        rightPurchased = false;
    }

    private void Start()
    {
        //upgradeButton.SetActive(false);
    }

    public void UpgradeSelected() //Remember that if you rename this function, you will need to reasign it for the button in the inspector
    {
        if (upgradeIsSold == false)
        {
            //CHECK that there is enough scrap
            //If yes:
            upgrade.UpgradePurchased(); //Not yet, open the placement window first
            buttonImage.color = Color.grey;
            upgradeIsSold = true;
            upgradeCost.text = "Sold";
            upgradeMenu.CloseUpgradePanel();
        }
        else
            Debug.Log("Upgrade is already purchased");
            return;
    }
}
