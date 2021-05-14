using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeStation : MonoBehaviour
{
    [SerializeField]
    GameObject UpgradeButton;
    public bool playerDead;

    private void Start()
    {
        UpgradeButton.SetActive(false);
        playerDead = false;
    }

    private void OnEnable()
    {
        scrPlayerHealth.OnPlayerDeath += PlayerDied;
    }

    private void OnDisable()
    {
        scrPlayerHealth.OnPlayerDeath -= PlayerDied;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            if (!playerDead)
            {
                UpgradeButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            if (!playerDead)
            {
                UpgradeButton.SetActive(false);
            }
        }
    }

    void PlayerDied()
    {
        playerDead = true;
    }
}
