using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class scrUpgradeButton : MonoBehaviour
{
    public static Action<UpgradesSO> OnUpgradeSelected; //Used to send a reference of the upgrade purchased to the upgradeUpgrade menu
    private scrGameManager gameManager;
    private scrPlacementButton currentPlacementButton;
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
    private int placement;
    [SerializeField] private scrUpgradeTheUpgradeButton leftUpgradeInfo; //Try assigning these through code, if this works
    [SerializeField] private scrUpgradeTheUpgradeButton rightUpgradeInfo;
    private Vector3 playerPossition;
    public static event Action<int, int> OnWeaponPurchased; //Weapon _ID, Weapon placement(left or right)


    private void Awake()
    {
        upgrade.ResetUpgradeStats();
        buttonImage = GetComponent<Image>();
        upgradeIsSold = false;
        upgradeButton = this.gameObject; //Self-assign as gameobject
        upgradeCost.text = upgrade.UpgradeCost.ToString();
        upgradeName.text = upgrade.UpgradeName;
        upgradeMenu = FindObjectOfType<scrUpgradeMenu>(); //Get the instance (using a singletonpattern to get the instance)
        playerPossition = GameObject.FindGameObjectWithTag("PlayerBody").transform.position;
        gameManager = scrGameManager.instance; //Gets the instance of this singleton
    }

    public void UpgradeSelected() //Remember that if you rename this function, you will need to reasign it for the button in the inspector
    {
        if (upgradeIsSold == false)
        {
            //CHECK that there is enough scrap
            //If yes:
            if(upgrade.UpgradeCost > gameManager.PlayerScrap)
            {
                upgradeMenu.CloseUpgradePanel();
                print("Could not afford upgrade. The cost was: " + upgrade.UpgradeCost + " and the player had: " + gameManager.PlayerScrap);
                return;
            }
            currentPlacementButton = gameManager.CurrentPlacementButton; //Gets the current reference from the game manager
            gameManager.DissableLastUsedPlacementButton(currentPlacementButton);
            switch (placement)
            {
                case 0:
                    upgrade.UpgradePurchased(placement);
                    buttonImage.color = Color.grey;
                    upgradeIsSold = true;
                    upgradeCost.text = "Sold";
                    upgradeMenu.CloseUpgradePanel();
                    leftUpgradeInfo.UpdateTheUpgrade(upgrade);
                    OnWeaponPurchased?.Invoke(upgrade.ProjectileType, 0);
                    return;
                case 1:
                    upgrade.UpgradePurchased(placement);
                    buttonImage.color = Color.grey;
                    upgradeIsSold = true;
                    upgradeCost.text = "Sold";
                    upgradeMenu.CloseUpgradePanel();
                    rightUpgradeInfo.UpdateTheUpgrade(upgrade);
                    OnWeaponPurchased?.Invoke(upgrade.ProjectileType, 1);
                    return;
                default:
                    Debug.LogError("The placement value is incorrect when used for switch statement in the class scrUpgradeButton");
                    return;
            }
        }
        else
            Debug.Log("Upgrade is already purchased");
            return;
    }
    private void getPlacement(int _placement) //Gets the int value for the placement from the OnPlacementSelected action
    {
        placement = _placement;
        //print("UpgradeButton is assigned the followin value for int placement: " + placement);
    }

    private void OnEnable()
    {
        scrUpgradeMenu.OnPlacementSelected += getPlacement;
    }
    private void OnDisable()
    {
        scrUpgradeMenu.OnPlacementSelected -= getPlacement;
    }
}
