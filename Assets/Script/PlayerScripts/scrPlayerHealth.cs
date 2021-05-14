using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class scrPlayerHealth : MonoBehaviour
{
    public static Action OnPlayerShieldsDissabled, playerTookDamage;
    public static Action OnPlayerDeath;
    public static Action OnShieldsUpgraded;

    [SerializeField] private float invincebilityLength;
    [Tooltip("Set player health")]
    public int playerHealth;
    private GameObject playerMesh;
    public AudioSource playertakedamageaudio;
    public AudioSource playerdiesaudio;
    private bool canTakeDamage;
    private playerController controller;
    private scrUpgradeMenu upgradeMenu;
    private scrGameManager gameManager;
    private bool playerIsHitRecently;

    //For shields upgrades
    [Tooltip("Sets the max level for the shield")]
    [SerializeField] private int shieldMaxLevel;

    [Tooltip("Decides how fast the shields will regain at level 0")]
    [SerializeField]private float defaultShieldRegenTime;
    private float shieldRegenTime;
    [Tooltip("How much faster the shields regain per level of upgrade")]
    [SerializeField]private float shieldTimeReductionOnUpgrade;
    private float timeSinceShieldDestroyed;
    public int ShieldsLevel { get; private set; }
    [Tooltip("How much it costs to upgrade shields at level 1")]
    [SerializeField]private int shieldUpgradeCost;
    private int shieldsUpgradeCostIncremental = 25;

    [SerializeField] private TextMeshProUGUI shieldsCurrentLevelText;
    [SerializeField] private TextMeshProUGUI shieldsUpgradeCostText;
    [SerializeField] private TextMeshProUGUI shieldsRegenTimeText;


    //Linjen under er lagt til av August og burde slettes
    [SerializeField]
    GameObject Shield;
    [SerializeField]
    private int strongholdTimer;
    private bool invincible;

    ParticleSystem ps;
    private void Awake()
    {
        canTakeDamage = true;
        playerMesh = GameObject.FindGameObjectWithTag("PlayerMesh");
        controller = GetComponent<playerController>();
        upgradeMenu = scrUpgradeMenu.Instance;
        gameManager = scrGameManager.instance;
        strongholdTimer = 20;
        invincible = false;
        ps = transform.Find("DeathParticles").GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        ShieldsLevel = 0;
        shieldRegenTime = defaultShieldRegenTime;
        playerHealth = 2;
        shieldsCurrentLevelText.text = "Current shield level: " + ShieldsLevel.ToString();
        shieldsUpgradeCostText.text = shieldUpgradeCost.ToString();
        shieldsRegenTimeText.text = "Current shield regen time is: " + shieldRegenTime.ToString();
    }
    private void Update()
    {
        if (controller.turning)
        {
            canTakeDamage = false;
        }
        else if(!controller.turning && !playerIsHitRecently)
        {
            canTakeDamage = true;
        }
        if (invincible)
        {
            canTakeDamage = false;
        }
        ShieldsRegen();
        if (playerHealth <= 1)
            Shield.SetActive(false);
        else
            Shield.SetActive(true);

        //For testing
        if(Input.GetKey(KeyCode.Q))
        {
            print("Ouch!");
            playerHealth -= 1;
        }
    }
    #region Shields
    public void UpgradeShields(int _level) //Called from upgrade button
    {
        if(gameManager.PlayerScrap > shieldUpgradeCost && ShieldsLevel < shieldMaxLevel)
        {
            //Spend scrap
            gameManager.SpendScrap(shieldUpgradeCost);
            //Increase cost
            shieldUpgradeCost += shieldsUpgradeCostIncremental;
            //Increase level
            ShieldsLevel += _level;
            //Improve shields
            shieldRegenTime -= shieldTimeReductionOnUpgrade;
            //For playing upgrade sound
            OnShieldsUpgraded?.Invoke();
            //Update text
            shieldsUpgradeCostText.text = shieldUpgradeCost.ToString();
            shieldsCurrentLevelText.text = "Current shield level: " + ShieldsLevel.ToString();
            shieldsRegenTimeText.text = "Current shield regen time is: " + shieldRegenTime.ToString();
            if(ShieldsLevel >= shieldMaxLevel)
            {
                shieldsCurrentLevelText.text = "Max upgrade level reached!";
            }
        }
    }
    public void ShieldsRegen() //This is running in update and will slowly regain the shields
    {
        if(playerHealth <= 1)
        {
            timeSinceShieldDestroyed += Time.deltaTime;
            if(timeSinceShieldDestroyed >= shieldRegenTime)
            {
                timeSinceShieldDestroyed = 0f;
                playerHealth = 2;
            }
        }
    }
    #endregion
    public void TakeDamage()
    {
             if (canTakeDamage)
            {
                canTakeDamage = false;
                playerIsHitRecently = true;
                StartCoroutine(ResetCanTakeDamage(invincebilityLength));
                print("ouch!");
                playerHealth -= 1;
                timeSinceShieldDestroyed = 0f;
                playertakedamageaudio.Play();
                playerTookDamage?.Invoke();
                if (playerHealth <= 1)
                {
                    OnPlayerShieldsDissabled?.Invoke();
                }
                if (playerHealth <= 0)
                {
                    playerDies();
                }
            }

    }
    public void DealDamageToPlayer(int _amount) //For reference in other scripts
    {
            if (canTakeDamage)
            {
                canTakeDamage = false;
                playerIsHitRecently = true;
            timeSinceShieldDestroyed = 0f;
            StartCoroutine(ResetCanTakeDamage(invincebilityLength));
                playerHealth -= _amount;
                playerTookDamage?.Invoke();
            if (playerHealth <= 1)
                {
                    OnPlayerShieldsDissabled?.Invoke();
                }
                if (playerHealth <= 0)
                {
                    playerDies();
                }
            }
    }
    private void playerDies()
    {
        //Dissable player body
        playerMesh.SetActive(false);
        ps.Play();

        //Play explotion effect
        OnPlayerDeath?.Invoke();
        //Move the player to a new scene
        playerdiesaudio.Play();
    }
    private IEnumerator ResetCanTakeDamage(float timer)
    {
        yield return new WaitForSeconds(timer);
        canTakeDamage = true;
        playerIsHitRecently = false;
    }
    private void OnEnable()
    {
        scrEnemyProjectiles.OnPlayerDamagedByEnemy += TakeDamage;
        Stronghold.OnStrongholdTriggered += RunInvincible;
    }
    private void OnDisable()
    {
        scrEnemyProjectiles.OnPlayerDamagedByEnemy -= TakeDamage;
        Stronghold.OnStrongholdTriggered -= RunInvincible;
    }

    void RunInvincible()
    {
        StartCoroutine("Invincible");
    }

    IEnumerator Invincible()
    {
        /*if(playerHealth == 0)
        {
            playerHealth = 100;
            yield return new WaitForSeconds(strongholdTimer);
            playerHealth = 0;
        }
        else
        {
            var previousHealth = playerHealth;
            playerHealth = 100;
            yield return new WaitForSeconds(strongholdTimer);
            playerHealth = previousHealth;
        } */

        invincible = true;
        yield return new WaitForSeconds(strongholdTimer);
        invincible = false;
    }
}
