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

    private void Awake()
    {
        upgrade.ResetUpgradeStats();
        buttonImage = GetComponent<Image>();
        upgradeIsSold = false;
        upgradeButton = this.gameObject; //Self-assign as gameobject
        upgradeCost.text = upgrade.UpgradeCost.ToString();
        upgradeName.text = upgrade.UpgradeName;
    }

    private void Start()
    {
        //upgradeButton.SetActive(false);
    }
    public void UpgradePurchased()
    {
        if (upgradeIsSold == false)
        {
            upgrade.UpgradePurchased();
            buttonImage.color = Color.grey;
            upgradeIsSold = true;
            upgradeCost.text = "Sold";
        }
        else
            Debug.Log("Upgrade is already purchased");
            return;
    }
}
