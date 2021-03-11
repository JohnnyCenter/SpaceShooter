using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class scrUpgradeTheUpgradeButton : MonoBehaviour
{
    private Image UpgradeImage; //Used to display a sketch of the upgrade purchased
    [SerializeField] private TextMeshProUGUI upgradeLevelText; //Used to tell display the CURRENT level of the upgrade
    [SerializeField] private TextMeshProUGUI upgradeCost;
    private int upgradeLevel;

    private void Awake()
    {
        UpgradeImage = GetComponent<Image>();
        upgradeLevel = 0;
    }
    private void Start()
    {
        upgradeLevelText.text = "Level " + upgradeLevel.ToString();
        upgradeCost.text = "0";
    }
    private void UpdateTheUpgrade(UpgradesSO _upgrade)
    {
        print("UpdatedTheUpgrade");
        upgradeLevel += 1;
        upgradeLevelText.text = "Level " + upgradeLevel.ToString();
        upgradeCost.text = _upgrade.UpgradeCost.ToString();
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
}
