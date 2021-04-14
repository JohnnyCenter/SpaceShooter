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
}
