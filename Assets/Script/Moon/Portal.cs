using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    int moonCounter, moonGoal; [Tooltip("Set when the Portal should open. When MoonCounter is the same as MoonGoal so will the portal open")]
    bool portalOpen;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        moonCounter = 0;
    }

    private void OnEnable()
    {
        Moon.MoonCompleted += AddToMoonCounter;
    }

    private void OnDisable()
    {
        Moon.MoonCompleted -= AddToMoonCounter;
    }

    private void Update()
    {
        if(moonCounter >= moonGoal)
        {
            OpenPortal();
        }
    }

    void OpenPortal()
    {
        portalOpen = true;
        sr.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody") && portalOpen)
        {
            Debug.Log("Player Won!");
        }
    }

    void AddToMoonCounter()
    {
        moonCounter += 1;
    }
}
