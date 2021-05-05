using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// This is a singleton, do not place this class on any objects with the exception of the "upgradePanelManager".
/// </summary>
public class scrUpgradeMenu : MonoBehaviour
{
    private static scrUpgradeMenu instance; //Make this upgrade menu into a singleton

    public static scrUpgradeMenu Instance { get { return instance; }}
    private AudioSource audioSource;
    public AudioClip cannotAfford;
    public AudioClip canAfford;

    [Header("Assign the menu button")]
    [Tooltip("Drag the openUpgradeMenu (found in the inspector under UI -> Canvas) into this slot")]
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject purchaseShieldsButton;

    [Header("Assign panels from the inspector")]
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject modelPanel; //Don`t forget to set this in the inspector!
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject upgradePanel; //Don`t forget to set this in the inspector!
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject upgradePlacementPanel; //Don`t forget to set this in the inspector!
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject purchasePanel; //Don`t forget to set this in the inspector!
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject upgradeTheUpgradePanel; //Don`t forget to set this in the inspector!
    [Header("Enemy Info cards")]
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject BasicEnemyInfoCard; //Don`t forget to set this in the inspector!
    private bool basicEnemyInfoCardHasBeenOpened;
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject mineInfoCard; //Don`t forget to set this in the inspector!
    private bool mineInfoCardHasBeenOpened;
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject lakituInfoCard; //Don`t forget to set this in the inspector!
    private bool lakituInfoCardHasBeenOpened;
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject beybladeInfoCard; //Don`t forget to set this in the inspector!
    private bool beybladeInfoCardHasBeenOpened;
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject suicideBomberInfoCard; //Don`t forget to set this in the inspector!
    private bool suicideBomberInfoCardHasBeenOpened;
    [Tooltip("These panels are all found under UI -> Canvas in the inspector")]
    [SerializeField] private GameObject bigBrotherInfoCard; //Don`t forget to set this in the inspector!
    private bool bigBrotherInfoCardHasBeenOpened;

    [SerializeField] private GameObject map;


    public GameObject UpgradePLacementPanel { get; private set; }
    //public int UpgradePlacement { get; private set; } //Buttons rerference this variable for upgrade placement
    public static Action<int> OnPlacementSelected;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); //Gets the instance
        UpgradePLacementPanel = upgradePlacementPanel;
        //UpgradePlacement = 0; //Assign a default value

        if(instance != null && instance != this)
        {
            Destroy(this.gameObject); //We do not want duplicates of this class
        }
        else
        {
            instance = this;
        }
        if(upgradePanel == null || upgradePlacementPanel == null) //Make sure these are assigned!
        {
            Debug.LogError("upgradePanel and/or upgradePlacementPanel is not assigned! Drag those UI panels to the corresponding fields in the UpgradePanelManager found in the inspector!");
        }
        //Set all pannels to active initialy so that local classes can run their "Awake" method to initialize their vars
        purchasePanel.SetActive(true);
        upgradePanel.SetActive(true);
        UpgradePLacementPanel.SetActive(true);
        upgradeTheUpgradePanel.SetActive(true);
        menuButton.SetActive(false);
    }

    private void Start()
    {
        purchaseShieldsButton.SetActive(false);
        //Set all panels to unactive, so the menu is "closed" at game start
        purchasePanel.SetActive(false);
        upgradePanel.SetActive(false);
        UpgradePLacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
        modelPanel.SetActive(false);

        //Reset info cards
        CloseAllInfoCards();
        basicEnemyInfoCardHasBeenOpened = false;
        mineInfoCardHasBeenOpened = false;
        lakituInfoCardHasBeenOpened = false;
        beybladeInfoCardHasBeenOpened = false;
        suicideBomberInfoCardHasBeenOpened = false;
        bigBrotherInfoCardHasBeenOpened = false;
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    purchasePanel.SetActive(true);
        //}
    }
    public void DisplayModelPanel()
    {
        modelPanel.SetActive(true);
    }
    public void TurnPurchaseShieldOff()
    {
        purchaseShieldsButton.SetActive(false);
    }
    public void OpenEnemyInfoCardByType(int index)
    {
        switch(index)
        {
            case 1:
                if(!basicEnemyInfoCardHasBeenOpened)
                {
                    Time.timeScale = 0f;
                    basicEnemyInfoCardHasBeenOpened = true;
                    BasicEnemyInfoCard.SetActive(true);
                    return;
                }
                return;
            case 2:
                if (!mineInfoCardHasBeenOpened)
                {
                    Time.timeScale = 0f;
                    mineInfoCardHasBeenOpened = true;
                    mineInfoCard.SetActive(true);
                    return;
                }
                return;
            case 3:
                if (!lakituInfoCardHasBeenOpened)
                {
                    Time.timeScale = 0f;
                    lakituInfoCardHasBeenOpened = true;
                    lakituInfoCard.SetActive(true);
                    return;
                }
                return;
            case 4:
                if (!beybladeInfoCardHasBeenOpened)
                {
                    Time.timeScale = 0f;
                    beybladeInfoCardHasBeenOpened = true;
                    beybladeInfoCard.SetActive(true);
                    return;
                }
                return;
            case 5:
                if (!suicideBomberInfoCardHasBeenOpened)
                {
                    Time.timeScale = 0f;
                    suicideBomberInfoCardHasBeenOpened = true;
                    suicideBomberInfoCard.SetActive(true);
                    return;
                }
                return;
            case 6:
                if (!bigBrotherInfoCardHasBeenOpened)
                {
                    Time.timeScale = 0f;
                    bigBrotherInfoCardHasBeenOpened = true;
                    bigBrotherInfoCard.SetActive(true);
                    return;
                }
                return;
        }
    }
    public void CloseAllInfoCards()
    {
        BasicEnemyInfoCard.SetActive(false);
        mineInfoCard.SetActive(false);
        lakituInfoCard.SetActive(false);
        beybladeInfoCard.SetActive(false);
        suicideBomberInfoCard.SetActive(false);
        bigBrotherInfoCard.SetActive(false);
        Time.timeScale = 1f;
        audioSource.Play();
    }
    public void OpenUpgradeMenu() //Called from the "Menu" button
    {
        if(purchasePanel.activeSelf == false && upgradePanel.activeSelf == false && UpgradePLacementPanel.activeSelf == false && upgradeTheUpgradePanel.activeSelf == false)
        {
            //Debug.Log("Opens upgrade panel");
            map.SetActive(false);
            purchasePanel.SetActive(true);
            menuButton.SetActive(false); //Hide the open menu button
            Time.timeScale = 0f;
            audioSource.Play();

        }
    }
    public void OpenUpgradeTheUpgradePanel()
    {
        upgradeTheUpgradePanel.SetActive(true);
        purchasePanel.SetActive(false);
        audioSource.Play();
        menuButton.SetActive(false); //Hide the open menu button

        Time.timeScale = 0f;

    }
    public void OpenUpgradePlacementPanel()
    {
        purchasePanel.SetActive(false);
        UpgradePLacementPanel.SetActive(true);
        audioSource.Play();
        menuButton.SetActive(false); //Hide the open menu button

        Time.timeScale = 0f;

    }
    public void OpenUpgradeMenuWithPlacementReference(int placement)
    {
        map.SetActive(false);
        upgradePanel.SetActive(true);
        //print("Upgrade menu is opened with the following placement: " + placement);
        //UpgradePlacement = placement;
        OnPlacementSelected?.Invoke(placement);
        audioSource.Play();
        menuButton.SetActive(false); //Hide the open menu button

        Time.timeScale = 0f;

    }
    public void ReturnToMainMenuNoSound() //Returns the player to the main menu
    {
        purchasePanel.SetActive(true);
        upgradePanel.SetActive(false);
        upgradePlacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
        menuButton.SetActive(false); //Hide the open menu button
        modelPanel.SetActive(false);
    }
    public void ReturnToMainMenu() //Returns the player to the main menu
    {
        purchasePanel.SetActive(true);
        upgradePanel.SetActive(false);
        upgradePlacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
        menuButton.SetActive(false); //Hide the open menu button
        modelPanel.SetActive(false);
        audioSource.Play();
    }
    public void CloseUpgradePanel() //CLoses the menu completely
    {
        map.SetActive(true);
        purchasePanel.SetActive(false);
        upgradePanel.SetActive(false);
        upgradePlacementPanel.SetActive(false);
        upgradeTheUpgradePanel.SetActive(false);
        menuButton.SetActive(true);
        modelPanel.SetActive(false);
        Time.timeScale = 1f;
        audioSource.Play();
    }
    private void ToggleOnShieldsButton()
    {
        purchaseShieldsButton.SetActive(true);
    }
    private void OnEnable()
    {
        scrPlayerHealth.OnPlayerShieldsDissabled += ToggleOnShieldsButton;
    }
    private void OnDisable()
    {
        scrPlayerHealth.OnPlayerShieldsDissabled -= ToggleOnShieldsButton;
    }
}
