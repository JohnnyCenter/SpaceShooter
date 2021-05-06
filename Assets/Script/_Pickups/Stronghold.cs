using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stronghold : MonoBehaviour
{
    public static Action OnStrongholdTriggered;

    private void OnEnable()
    {
        Comet.ActivatePowerUp += TriggerStronghold;
    }

    private void OnDisable()
    {
        Comet.ActivatePowerUp -= TriggerStronghold;
    }

    private void TriggerStronghold()
    {
        OnStrongholdTriggered();
    }
}
