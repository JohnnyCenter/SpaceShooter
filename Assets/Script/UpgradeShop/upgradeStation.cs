using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeStation : MonoBehaviour
{
    [SerializeField]
    GameObject UpgradeButton;

    private void Start()
    {
        UpgradeButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            UpgradeButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            UpgradeButton.SetActive(false);
        }
    }
}
