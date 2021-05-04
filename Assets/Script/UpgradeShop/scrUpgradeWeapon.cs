using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scrUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponLevel0;
    [SerializeField] private GameObject weaponLevel1;
    [SerializeField] private GameObject weaponLevel2;
    [SerializeField] private GameObject weaponLevel3;
    [SerializeField] private Image upgradeImageLevel2;
    [SerializeField] private Image upgradeImageLevel3;
    [SerializeField] private Image upgradeImageLevel4;
    [SerializeField] private TextMeshProUGUI upgradeInfoLevel2;
    [SerializeField] private TextMeshProUGUI upgradeInfoLevel3;
    [SerializeField] private TextMeshProUGUI upgradeInfoLevel4;
    public Image UpgradeImageLevel2 { get; private set; }
    public Image UpgradeImageLevel3 { get; private set; }
    public Image UpgradeImageLevel4 { get; private set; }
    public TextMeshProUGUI UpgradeInfoLevel2 { get; private set; }
    public TextMeshProUGUI UpgradeInfoLevel3 { get; private set; }
    public TextMeshProUGUI UpgradeInfoLevel4 { get; private set; }

    private int upgradeLevel;
    private int upgradePlacement;

    private void Start()
    {
        upgradeLevel = 0;
        weaponLevel1.SetActive(false);
        weaponLevel2.SetActive(false);
        weaponLevel3.SetActive(false);
        AssignSprites();
    }
    public void AssignSprites()
    {
        UpgradeImageLevel2 = upgradeImageLevel2;
        UpgradeImageLevel3 = upgradeImageLevel3;
        UpgradeImageLevel4 = upgradeImageLevel4;
        UpgradeInfoLevel2 = upgradeInfoLevel2;
        UpgradeInfoLevel3 = upgradeInfoLevel3;
        UpgradeInfoLevel4 = upgradeInfoLevel4;
    }
    public void UpgradeWeapon()
    {
        //print("Weapon is upgraded---");
        upgradeLevel += 1;
        switch(upgradeLevel)
        {
            case 1:
                //print("---To level 1");
                weaponLevel1.SetActive(true);
                return;
            case 2:
                //print("---To level 2");
                weaponLevel2.SetActive(true);
                return;
            case 3:
                //print("---To level 3");
                weaponLevel3.SetActive(true);
                return;
        }
    }
}
