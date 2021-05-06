using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.PostProcessing;
public class PowerUpUIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] PowerUpText;
    [SerializeField]
    GameObject[] PowerUpImage;
    private int powerType, powerUpTime;
    public static event Action SabotageSFX, ScrapperSFX, StrongholdSFX, ScavengerSFX;
    Image CosmicEffect;
    PostProcessVolume pp;
    private float tweakNumber, desiredNumber, initialNumber;
    private bool postProcessing;

    ColorGrading cg;

    private void Awake()
    {
        foreach (TextMeshProUGUI text in PowerUpText)
        {
            text.enabled = false;
        }

        foreach(GameObject image in PowerUpImage)
        {
            image.gameObject.SetActive(false);
        }

        CosmicEffect = GetComponent<Image>();
        CosmicEffect.color = Color.clear;
        pp = GetComponentInChildren<PostProcessVolume>();
        initialNumber = 0;
        desiredNumber = 50;
        tweakNumber = 0;

        pp.profile.TryGetSettings(out cg);
    }

    private void OnEnable()
    {
        scrSabotage.OnSabotageTriggered += Sabotage;
        Stronghold.OnStrongholdTriggered += StrongholdPower;
        scrScrapper.OnScrapperTriggered += Scrapper;
        scrScavenger.OnScavengerTriggered += Scavenger;
    }

    private void OnDisable()
    {
        scrSabotage.OnSabotageTriggered -= Sabotage;
        Stronghold.OnStrongholdTriggered -= StrongholdPower;
        scrScrapper.OnScrapperTriggered -= Scrapper;
        scrScavenger.OnScavengerTriggered -= Scavenger;
    }

    void Sabotage()
    {
        powerType = 0;
        powerUpTime = 5;
        PowerUpImage[0].SetActive(true);
        PowerUpText[0].enabled = true;
        StartCoroutine("Sequence");
        SabotageSFX?.Invoke();
        Debug.Log("POWER-UP: Sabotage Activated");
    }

    void StrongholdPower()
    {
        powerType = 1;
        powerUpTime = 15;
        PowerUpImage[1].SetActive(true);
        PowerUpText[1].enabled = true;
        StartCoroutine("Sequence");
        StrongholdSFX?.Invoke();
        Debug.Log("POWER-UP: Stronghold Activated");
    }

    void Scrapper()
    {
        powerType = 2;
        powerUpTime = 15;
        PowerUpImage[2].SetActive(true);
        PowerUpText[2].enabled = true;
        StartCoroutine("Sequence");
        ScrapperSFX?.Invoke();
        Debug.Log("POWER-UP: Scrapper Activated");
    }

    void Scavenger()
    {
        powerType = 3;
        powerUpTime = 1;
        PowerUpImage[3].SetActive(true);
        PowerUpText[3].enabled = true;
        StartCoroutine("Sequence");
        ScavengerSFX?.Invoke();
        Debug.Log("POWER-UP: Scavenger Activated");
    }

    IEnumerator Sequence()
    {
        CosmicEffectOn();
        Debug.Log("POWER-UP: Animation begun");
        PowerUpText[powerType].rectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f);
        var image = PowerUpImage[powerType].GetComponent<Image>();
        image.DOColor(Color.white, 1f);
        postProcessing = true;
        yield return new WaitForSeconds(1.5f);
        desiredNumber = 0;
        CosmicEffectOff();
        PowerUpText[powerType].rectTransform.DOAnchorPos(new Vector2(-700, 0), 0.5f);
        yield return new WaitForSeconds(powerUpTime);
        var flashEffect = PowerUpImage[powerType].GetComponent<FlashingColor>();
        if(flashEffect != null)
        {
            flashEffect.flashing = true;
        }
        yield return new WaitForSeconds(3f);
        flashEffect.flashing = false;
        PowerUpText[powerType].rectTransform.anchoredPosition = new Vector2(650, 0);
        image.color = Color.clear;
    }

    void CosmicEffectOn()
    {
        CosmicEffect.DOColor(Color.white, 0.5f);
    }

    void CosmicEffectOff()
    {
        CosmicEffect.DOColor(Color.clear, 0.5f);
    }


    private void Update()
    {
        if (postProcessing)
        {
            if (tweakNumber != desiredNumber)
            {
                if (initialNumber < desiredNumber)
                {
                    tweakNumber += (10 * Time.deltaTime) * (desiredNumber - initialNumber);
                    if (tweakNumber >= desiredNumber)
                        tweakNumber = desiredNumber;
                }
                else
                {
                    tweakNumber -= (30 * Time.deltaTime) * (initialNumber - desiredNumber);
                    if (tweakNumber <= desiredNumber)
                        tweakNumber = desiredNumber;
                }
            }
            cg.saturation.value = tweakNumber;
        }
    }
}
