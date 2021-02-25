using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class scrUpgradeButton : MonoBehaviour
{
    private GameObject upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    private bool upgradeIsSold;
    private Image buttonImage;
    [Header("Define upgrade")]
    [SerializeField] private UpgradesSO upgrade;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        upgradeIsSold = false;
        upgradeButton = this.gameObject; //Self-assign as gameobject
        upgradeCost.text = 10.ToString();
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
