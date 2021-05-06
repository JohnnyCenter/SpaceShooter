using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrScavenger : MonoBehaviour
{
    public static Action OnScavengerTriggered;

    private void OnEnable()
    {
        Comet.ActivatePowerUp += TriggerScavenger;
    }

    private void OnDisable()
    {
        Comet.ActivatePowerUp -= TriggerScavenger;
    }

    private void TriggerScavenger()
    {
        OnScavengerTriggered();
    }
}
