using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class scrPlacementButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI placementText;
    private Image buttonImage;
    private scrUpgradeMenu upgrademenu;
    [Tooltip("0 is left placement, 1 is middle and 2 is right")]
    [Range(0, 2)]
    [SerializeField] int upgradePlacementNumber;
    private bool placementUsed;
    private void Awake()
    {
        upgrademenu = FindObjectOfType<scrUpgradeMenu>(); //Get the instance, Can do this because the upgrade menu is a singleton
        placementUsed = false;
        buttonImage = GetComponent<Image>();
    }
    public void ButtonClicked()
    {
        if(placementUsed == false)
        {
            upgrademenu.OpenUpgradeMenuWithPlacementReference(upgradePlacementNumber);
            placementUsed = true;
            placementText.text = "Used";
            upgrademenu.UpgradePLacementPanel.SetActive(false); //Close the upgrade placement panel
            buttonImage.color = Color.grey;
        }
        else
        {
            Debug.Log("placementUsed");
        }
    }
}
