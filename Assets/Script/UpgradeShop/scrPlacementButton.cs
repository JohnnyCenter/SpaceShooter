using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class scrPlacementButton : MonoBehaviour
{
    public static Action<scrPlacementButton> OnPlacementButtonUsed;
    [SerializeField] TextMeshProUGUI placementText;
    private Image buttonImage;
    private scrUpgradeMenu upgrademenu;
    [Tooltip("0 is left placement, 1 is right")]
    [Range(0, 1)]
    [SerializeField] int upgradePlacementNumber;
    private bool placementUsed;
    private bool buttonIsUsable;
    private void Awake()
    {
        upgrademenu = FindObjectOfType<scrUpgradeMenu>(); //Get the instance, Can do this because the upgrade menu is a singleton
        placementUsed = false;
        buttonImage = GetComponent<Image>();
        buttonIsUsable = true;
    }

    public void DissableButton()
    {
        placementUsed = true;
        placementText.text = "Used";
        buttonImage.color = Color.grey;
    }
    public void ButtonClicked()
    {
        if(placementUsed == false)
        {
            //print("Placement button clicked");
            OnPlacementButtonUsed?.Invoke(this);
            upgrademenu.OpenUpgradeMenuWithPlacementReference(upgradePlacementNumber);
            upgrademenu.UpgradePLacementPanel.SetActive(false); //Close the upgrade placement panel
        }
        else
        {
            Debug.Log("placementUsed");
        }
    }
    public void SetButtonToNotActive()
    {
        //print("Button is set to not active");
        buttonIsUsable = false;
    }
    private void CheckIfActive()
    {
        if(buttonIsUsable)
        {
            return;
        }
        else
        DissableButton();
    }
    private void OnEnable()
    {
        CheckIfActive();
    }
}
