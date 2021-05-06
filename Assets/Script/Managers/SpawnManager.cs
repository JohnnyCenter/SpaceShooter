using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    scrCircleSpawner CircleSpawner;

    private void Awake()
    {
        CircleSpawner = FindObjectOfType<scrCircleSpawner>();
    }

    private void Start()
    {
        CircleSpawner.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Moon.PlayerEnterMoonZoneGreen += ActivateCircleSpawner;
        Moon.PlayerEnterMoonZoneRed += ActivateCircleSpawner;
        Moon.PlayerEnterMoonZoneYellow += ActivateCircleSpawner;
        Moon.PlayerExitMoonZone += DeactivateCircleSpawner;
    }

    private void OnDisable()
    {
        Moon.PlayerEnterMoonZoneGreen -= ActivateCircleSpawner;
        Moon.PlayerEnterMoonZoneRed -= ActivateCircleSpawner;
        Moon.PlayerEnterMoonZoneYellow -= ActivateCircleSpawner;
        Moon.PlayerExitMoonZone -= DeactivateCircleSpawner;
    }

    void ActivateCircleSpawner()
    {
        CircleSpawner.gameObject.SetActive(true);
    }

    void DeactivateCircleSpawner()
    {
        CircleSpawner.gameObject.SetActive(false);
    }
}
