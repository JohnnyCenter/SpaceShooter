using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrapperEffect : MonoBehaviour
{
    ParticleSystem Flashy;

    private void Awake()
    {
        Flashy = GetComponentInChildren<ParticleSystem>();
    }

    void TurnOnEffect()
    {
        StartCoroutine("Effect");
    }

    IEnumerator Effect()
    {
        Flashy.Play();
        yield return new WaitForSeconds(20);
        Flashy.Stop();
    }

    private void OnEnable()
    {
        scrScrapper.OnScrapperTriggered += TurnOnEffect;
    }

    private void OnDisable()
    {
        scrScrapper.OnScrapperTriggered -= TurnOnEffect;
    }
}
