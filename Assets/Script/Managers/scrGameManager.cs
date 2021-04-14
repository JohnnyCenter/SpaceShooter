using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scrGameManager : MonoBehaviour
{
    [SerializeField] private GameObject respawnPanel;
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
        scrapText.text =  PlayerScrap.ToString();
        totalNumberOfEnemiesKilled = 0;
        respawnPanel.SetActive(false);
    }
    public void SpendScrap(int _amount)
    {
        PlayerScrap -= _amount;
        scrapText.text =  PlayerScrap.ToString();
    }
    private void ScrapGained(int _amount)
    {
        PlayerScrap += _amount; //Update scrap amount
        scrapText.text =  PlayerScrap.ToString(); //Update display
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
    private void OpenRespawnPanel()
    {
        respawnPanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnEnable()
    {
        scrPlacementButton.OnPlacementButtonUsed += GetPlacementButtonUsed;
        scrEnemyStats.OnEnemyKilled += ScrapGained;
        scrPlayerHealth.OnPlayerDeath += OpenRespawnPanel;
    }
    private void OnDisable()
    {
        scrEnemyStats.OnEnemyKilled -= ScrapGained;
        scrPlacementButton.OnPlacementButtonUsed -= GetPlacementButtonUsed;
        scrPlayerHealth.OnPlayerDeath -= OpenRespawnPanel;
    }
}
