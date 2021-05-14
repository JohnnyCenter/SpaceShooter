using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScravengerHandler : MonoBehaviour
{
    private scrPlayerHealth health;
    private scrGameManager scraps;
    ParticleSystem Flashy;
    [SerializeField]
    GameObject SecondEffect;
    ParticleSystem Flashy2;

    private void Awake()
    {
        health = gameObject.GetComponentInParent<scrPlayerHealth>();
        scraps = FindObjectOfType<scrGameManager>();
        Flashy = GetComponentInChildren<ParticleSystem>();
        Flashy2 = SecondEffect.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        scrScavenger.OnScavengerTriggered += Resupply;
    }

    private void OnDisable()
    {
        scrScavenger.OnScavengerTriggered -= Resupply;
    }

    void Resupply()
    {
        if (health.playerHealth == 1)
        {
            health.ShieldsRegen();
            StartCoroutine("ShipParticles");
        }
        else
        {
            scraps.BonusScrap();
            StartCoroutine("ScrapParticles");
        }
    }

    IEnumerator ShipParticles()
    {
        Flashy.Play();
        Debug.Log("Particles are there");
        yield return new WaitForSeconds(5);
        Flashy.Stop();
    }

    IEnumerator ScrapParticles()
    {
        Flashy2.Play();
        Debug.Log("Particles are there");
        yield return new WaitForSeconds(5);
        Flashy2.Stop();
    }
}
