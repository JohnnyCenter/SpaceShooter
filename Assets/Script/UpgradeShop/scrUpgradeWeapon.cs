using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponLevel0;
    [SerializeField] private GameObject weaponLevel1;
    [SerializeField] private GameObject weaponLevel2;
    [SerializeField] private GameObject weaponLevel3;
    private int upgradeLevel;
    private int upgradePlacement;

    private void Start()
    {
        upgradeLevel = 0;
        weaponLevel1.SetActive(false);
        weaponLevel2.SetActive(false);
        weaponLevel3.SetActive(false);
    }
    public void UpgradeWeapon()
    {
        print("Weapon is upgraded---");
        upgradeLevel += 1;
        switch(upgradeLevel)
        {
            case 1:
                print("---To level 1");
                weaponLevel1.SetActive(true);
                return;
            case 2:
                print("---To level 2");
                weaponLevel2.SetActive(true);
                return;
            case 3:
                print("---To level 3");
                weaponLevel3.SetActive(true);
                return;
        }
    }
}
