using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrScrapper : MonoBehaviour
{
    public static Action OnScrapperTriggered;

    private void OnEnable()
    {
        Comet.ActivatePowerUp += TriggerScrapper;
    }

    private void OnDisable()
    {
        Comet.ActivatePowerUp -= TriggerScrapper;
    }

    private void TriggerScrapper()
    {
        OnScrapperTriggered();
    }
}
