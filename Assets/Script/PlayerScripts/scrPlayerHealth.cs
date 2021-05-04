using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrPlayerHealth : MonoBehaviour
{
    public static Action OnPlayerShieldsDissabled;
    [SerializeField] private float invincebilityLength;
    public static Action OnPlayerDeath;
    [Tooltip("Set player health")]
    [SerializeField] private int playerHealth;
    private GameObject playerMesh;
    public AudioSource playertakedamageaudio;
    public AudioSource playerdiesaudio;
    private bool canTakeDamage;
    private playerController controller;
    private scrUpgradeMenu upgradeMenu;
    private scrGameManager gameManager;
    private bool playerIsHitRecently;

    //Linjen under er lagt til av August og burde slettes
    [SerializeField]
    GameObject Shield;
    private void Awake()
    {
        canTakeDamage = true;
        playerMesh = GameObject.FindGameObjectWithTag("PlayerMesh");
        controller = GetComponent<playerController>();
        upgradeMenu = scrUpgradeMenu.Instance;
        gameManager = scrGameManager.instance;
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
        if (playerHealth <= 1)
            Shield.SetActive(false);
        else
            Shield.SetActive(true);
    }
    public void ShieldsRestored()
    {
        if(playerHealth == 1 && gameManager.PlayerScrap > 20)
        {
            gameManager.SpendScrap(20);
            playerHealth += 1;
            upgradeMenu.TurnPurchaseShieldOff();
        }
    }
    private void TakeDamage()
    {
        if(canTakeDamage)
        {
            canTakeDamage = false;
            playerIsHitRecently = true;
            StartCoroutine(ResetCanTakeDamage(invincebilityLength));
            print("ouch!");
            playerHealth -= 1;
            playertakedamageaudio.Play();
            if(playerHealth <= 1)
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
        if(canTakeDamage)
        {
            canTakeDamage = false;
            playerIsHitRecently = true;
            StartCoroutine(ResetCanTakeDamage(invincebilityLength));
            playerHealth -= _amount;
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
    }
    private void OnDisable()
    {
        scrEnemyProjectiles.OnPlayerDamagedByEnemy -= TakeDamage;
    }
}
