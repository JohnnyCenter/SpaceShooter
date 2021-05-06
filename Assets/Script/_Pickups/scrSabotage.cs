using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrSabotage : MonoBehaviour
{
    public static Action OnSabotageTriggered;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) //For testing
        {
            TriggerSabotage();
        }
    }

    private void OnEnable()
    {
        Comet.ActivatePowerUp += TriggerSabotage;
    }

    private void OnDisable()
    {
        Comet.ActivatePowerUp -= TriggerSabotage;
    }

    private void TriggerSabotage()
    {
        OnSabotageTriggered?.Invoke();
    }
}
