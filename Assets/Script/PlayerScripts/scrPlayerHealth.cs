using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrPlayerHealth : MonoBehaviour
{
    public static Action OnPlayerDeath;
    [Tooltip("Set player health")]
    [SerializeField] private int playerHealth;
    private GameObject playerMesh;

    //Linjen under er lagt til av August og burde slettes
    [SerializeField]
    GameObject Shield;
    private void Awake()
    {
        playerMesh = GameObject.FindGameObjectWithTag("PlayerMesh");
    }
    private void TakeDamage()
    {
        print("ouch!");
        playerHealth -= 1;
        if(playerHealth <= 0)
        {
            playerDies();
        }
    }
    public void DealDamageToPlayer(int _amount) //For reference in other scripts
    {
        playerHealth -= _amount;
        if (playerHealth <= 0)
        {
            playerDies();
        }
    }
    private void playerDies()
    {
        //Dissable player body
        playerMesh.SetActive(false);

        //Play explotion effect
        OnPlayerDeath?.Invoke();
        //Move the player to a new scene
    }
    private void OnEnable()
    {
        scrEnemyProjectiles.OnPlayerDamagedByEnemy += TakeDamage;
    }
    private void OnDisable()
    {
        scrEnemyProjectiles.OnPlayerDamagedByEnemy -= TakeDamage;
    }

    //ALT UNDER ER MIDLERTIDIG LAGT TIL AV AUGUST

    private void Update()
    {
        if (playerHealth <= 1)
            Shield.SetActive(false);
        else
            Shield.SetActive(true);
    }
}
