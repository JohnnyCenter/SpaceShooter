using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scrGameManager : MonoBehaviour
{
    public static scrGameManager instance;
    public scrPlacementButton CurrentPlacementButton { get; set; }
    [SerializeField] private TextMeshProUGUI scrapText;
    public int PlayerScrap { get; private set; }
    private int totalNumberOfEnemiesKilled;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        scrapText.text = "Current scrap is: " + PlayerScrap.ToString();
        totalNumberOfEnemiesKilled = 0;
    }
    private void ScrapGained(int _amount)
    {
        PlayerScrap += _amount; //Update scrap amount
        scrapText.text = "Current scrap is: " + PlayerScrap.ToString(); //Update display
        totalNumberOfEnemiesKilled += 1; //Increment total number of enemies killed
    }
    private void GetPlacementButtonUsed(scrPlacementButton _buttonSent)
    {
        //print("got placement button");
        CurrentPlacementButton = _buttonSent; //Updates the reference of the placement button
    }
    public void DissableLastUsedPlacementButton(scrPlacementButton _buttonSent)
    {
        _buttonSent.SetButtonToNotActive();
    }
    private void OnEnable()
    {
        scrPlacementButton.OnPlacementButtonUsed += GetPlacementButtonUsed;
        scrEnemyStats.OnEnemyKilled += ScrapGained;
    }
    private void OnDisable()
    {
        scrEnemyStats.OnEnemyKilled -= ScrapGained;
        scrPlacementButton.OnPlacementButtonUsed -= GetPlacementButtonUsed;
    }
}
