using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageEffect : MonoBehaviour
{
    ParticleSystem Electricity;
    private bool inRange;

    private void Awake()
    {
        Electricity = GetComponentInChildren<ParticleSystem>();
    }

    private void OnBecameVisible()
    {
        inRange = true;
    }

    private void OnBecameInvisible()
    {
        inRange = false;
    }

    void TurnOnEffect()
    {
        if (inRange)
        {
            StartCoroutine("Effect");
        }
    }

    IEnumerator Effect()
    {
        Electricity.Play();
        yield return new WaitForSeconds(10);
        Electricity.Stop();
    }

    private void OnEnable()
    {
        scrSabotage.OnSabotageTriggered += TurnOnEffect;
    }

    private void OnDisable()
    {
        scrSabotage.OnSabotageTriggered -= TurnOnEffect;
    }
}
