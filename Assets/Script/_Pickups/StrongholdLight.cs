using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StrongholdLight : MonoBehaviour
{
    Light InvincibleLight;

    private void Awake()
    {
        InvincibleLight = GetComponent<Light>();
        InvincibleLight.intensity = 0;
    }

    private void OnEnable()
    {
        Stronghold.OnStrongholdTriggered += TurnOnLight;
    }
    private void OnDisable()
    {
        Stronghold.OnStrongholdTriggered -= TurnOnLight;
    }

    void TurnOnLight()
    {
        InvincibleLight.DOIntensity(50, 2);
        StartCoroutine("TurnOffLight");
    }

    IEnumerator TurnOffLight()
    {
        yield return new WaitForSeconds(18);
        InvincibleLight.DOIntensity(0, 2);
    }
}
