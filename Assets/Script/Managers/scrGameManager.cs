using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scrGameManager : MonoBehaviour
{
    [SerializeField] private GameObject respawnPanel, winScreen;
    public static scrGameManager instance;
    public scrPlacementButton CurrentPlacementButton { get; set; }
    [SerializeField] private TextMeshProUGUI scrapText, loseScoreText, winScoreText, killText;
    public int PlayerScrap { get; private set; }
    private int totalNumberOfEnemiesKilled;

    [SerializeField] private float deathTime;
    private int enemyScore, moonScore, portalBonus, totalScore, moonsCompleted;

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
        PlayerScrap = 50;
        scrapText.text =  PlayerScrap.ToString();
        totalNumberOfEnemiesKilled = 0;
        killText.text = totalNumberOfEnemiesKilled.ToString();
        respawnPanel.SetActive(false);
        winScreen.SetActive(false);
        
        deathTime = 2;
        totalScore = 0;
        portalBonus = 0;
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
        killText.text = totalNumberOfEnemiesKilled.ToString();
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
        StartCoroutine("RunEndScreen");
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
        Moon.MoonCompleted += IncreaseMoonScore;
        Portal.PlayerEnteredPortal += PortalBonus;
    }
    private void OnDisable()
    {
        scrEnemyStats.OnEnemyKilled -= ScrapGained;
        scrPlacementButton.OnPlacementButtonUsed -= GetPlacementButtonUsed;
        scrPlayerHealth.OnPlayerDeath -= OpenRespawnPanel;
        Moon.MoonCompleted -= IncreaseMoonScore;
        Portal.PlayerEnteredPortal -= PortalBonus;
    }

    IEnumerator RunEndScreen()
    {
        yield return new WaitForSeconds(deathTime);
        CalculateScore();
        loseScoreText.text = "Score: " + totalScore.ToString();
        respawnPanel.SetActive(true);
    }

    IEnumerator RunWinScreen()
    {
        yield return new WaitForSeconds(deathTime);
        CalculateScore();
        winScoreText.text = "Score: " + totalScore.ToString();
        winScreen.SetActive(true);
    }

    void IncreaseMoonScore()
    {
        moonsCompleted += 1;
        moonScore += 1000 * moonsCompleted;
    }

    void PortalBonus()
    {
        portalBonus = 10000;
        StartCoroutine("RunWinScreen");
    }

    void CalculateScore()
    {
        enemyScore = totalNumberOfEnemiesKilled * 250;

        totalScore = enemyScore + moonScore + portalBonus;
    }
}
