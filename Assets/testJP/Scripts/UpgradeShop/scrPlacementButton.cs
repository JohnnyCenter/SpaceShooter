using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlacementButton : MonoBehaviour
{
    private scrUpgradeMenu upgrademenu;
    [Tooltip("0 is left placement, 1 is middle and 2 is right")]
    [Range(0, 2)]
    [SerializeField] int upgradePlacementNumber;

    private void Awake()
    {
        upgrademenu = FindObjectOfType<scrUpgradeMenu>(); //Get the instance, Can do this because the upgrade menu is a singleton
    }
    public void ButtonClicked()
    {
        //print("Button clicked, my placement is: " + upgradePlacementNumber);
        upgrademenu.UpgradePLacementPanel.SetActive(false); //Close the upgrade placement panel
        upgrademenu.OpenUpgradeMenuWithPlacementReference(upgradePlacementNumber);
    }
}
