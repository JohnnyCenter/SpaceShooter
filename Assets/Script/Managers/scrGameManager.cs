using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class scrGameManager : MonoBehaviour
{
    [SerializeField] private GameObject respawnPanel, winScreen, scrapImage;
    public static scrGameManager instance;
    public scrPlacementButton CurrentPlacementButton { get; set; }
    [SerializeField] private TextMeshProUGUI scrapText, UiScrapText1, UiScrapText2, UiScrapText3, loseScoreText, winScoreText, killText;
    public int PlayerScrap { get; private set; }
    private int totalNumberOfEnemiesKilled;

    [SerializeField] private float deathTime;
    private int enemyScore, moonScore, portalBonus, totalScore, moonsCompleted;

    private bool scrapperActive;
    [SerializeField]
    private int scrapperCooldown, scrapBonus;

    private float desiredNumber, initialNumber, currentNumber;
    private float animationTime = 0.5f;

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

        scrapperCooldown = 20;
    }

    private void Start()
    {
        PlayerScrap = 500;
        scrapText.text =  PlayerScrap.ToString();
        UiScrapText1.text = PlayerScrap.ToString();
        UiScrapText2.text = PlayerScrap.ToString();
        UiScrapText3.text = PlayerScrap.ToString();
        totalNumberOfEnemiesKilled = 0;
        killText.text = totalNumberOfEnemiesKilled.ToString();
        respawnPanel.SetActive(false);
        winScreen.SetActive(false);
        
        deathTime = 2;
        totalScore = 0;
        portalBonus = 0;

        initialNumber = currentNumber = 0f;
        desiredNumber = 0;
    }
    public void SpendScrap(int _amount)
    {
        PlayerScrap -= _amount;
        scrapText.text =  PlayerScrap.ToString();

        UiScrapText1.text = PlayerScrap.ToString();
        UiScrapText2.text = PlayerScrap.ToString();
        UiScrapText3.text = PlayerScrap.ToString();
    }
    private void ScrapGained(int _amount)
    {
        if (scrapperActive)
        {
            PlayerScrap += _amount * 2; //Add twice as much scrap while Power-Up is active
        }
        else
        {
            PlayerScrap += _amount; //Update scrap amount
        }
        scrapText.text =  PlayerScrap.ToString(); //Update display
        UiScrapText1.text = PlayerScrap.ToString();
        UiScrapText2.text = PlayerScrap.ToString();
        UiScrapText3.text = PlayerScrap.ToString();
        totalNumberOfEnemiesKilled += 1; //Increment total number of enemies killed
        killText.text = totalNumberOfEnemiesKilled.ToString();
        StartCoroutine("IncreaseFontSizeScrap");
        StartCoroutine("IncreaseFontSizeKill");
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
        scrScrapper.OnScrapperTriggered += RunScrapper;
    }
    private void OnDisable()
    {
        scrEnemyStats.OnEnemyKilled -= ScrapGained;
        scrPlacementButton.OnPlacementButtonUsed -= GetPlacementButtonUsed;
        scrPlayerHealth.OnPlayerDeath -= OpenRespawnPanel;
        Moon.MoonCompleted -= IncreaseMoonScore;
        Portal.PlayerEnteredPortal -= PortalBonus;
        scrScrapper.OnScrapperTriggered += RunScrapper;

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

    void RunScrapper()
    {
        Debug.Log("Scrapper is active");
        StartCoroutine("Scrapper");
    }

    IEnumerator Scrapper()
    {
        scrapperActive = true;
        var image = scrapImage.GetComponent<Image>();
        image.DOColor(Color.yellow, 2);
        Debug.Log("It got here");
        yield return new WaitForSeconds(scrapperCooldown);
        image.DOColor(Color.white, 1);
        scrapperActive = false;
    }

    public void BonusScrap()
    {
        PlayerScrap += scrapBonus;
        scrapText.text = PlayerScrap.ToString();
        UiScrapText1.text = PlayerScrap.ToString();
        UiScrapText2.text = PlayerScrap.ToString();
        UiScrapText3.text = PlayerScrap.ToString();
        StartCoroutine("IncreaseFontSizeScrap");
    }

    IEnumerator IncreaseFontSizeScrap()
    {
        scrapText.fontSize = 50;
        yield return new WaitForSeconds(1);
        scrapText.fontSize = 40;
    }

    IEnumerator IncreaseFontSizeKill()
    {
        killText.fontSize = 50;
        yield return new WaitForSeconds(1);
        killText.fontSize = 40;
    }

    private void Update()
    {
        if (currentNumber != desiredNumber)
        {
            if (initialNumber < desiredNumber)
            {
                currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                if (currentNumber >= desiredNumber)
                    currentNumber = desiredNumber;
            }
            else
            {
                currentNumber -= (animationTime * Time.deltaTime) * (initialNumber - desiredNumber);
                if (currentNumber <= desiredNumber)
                    currentNumber = desiredNumber;
            }
        }
        loseScoreText.text = "Score: " + currentNumber.ToString("0");
        winScoreText.text = "Score: " + currentNumber.ToString("0");
    }

    void AddToValue(float value)
    {
        initialNumber = currentNumber;
        desiredNumber += value;
    }

    IEnumerator RunEndScreen()
    {
        yield return new WaitForSeconds(deathTime);
        CalculateScore();
        respawnPanel.SetActive(true);
        //yield return new WaitForSeconds(2);
        AddToValue(totalScore);
        //loseScoreText.text = "Score: " + totalScore.ToString();
    }

    IEnumerator RunWinScreen()
    {
        yield return new WaitForSeconds(deathTime);
        CalculateScore();
        winScreen.SetActive(true);
        AddToValue(totalScore);
       // winScoreText.text = "Score: " + totalScore.ToString("0");
    }
}
