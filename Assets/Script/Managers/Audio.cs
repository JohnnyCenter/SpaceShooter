using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private bool inCombat;

    private void OnEnable()
    {
        Moon.MoonCompleted += DiscoverMoonAudio;
        Moon.PlayerEnterMoonZoneRed += EnteredMoonZone;
        Moon.PlayerEnterMoonZoneYellow += EnteredMoonZone;
        Moon.PlayerEnterMoonZoneGreen += EnteredMoonZone;
        Moon.PlayerExitMoonZone += ExitMoonZone;
        Portal.PlayerEnteredPortal += EnteredPortal;
    }

    private void OnDisable()
    {
        Moon.MoonCompleted -= DiscoverMoonAudio; 
        Moon.PlayerEnterMoonZoneRed -= EnteredMoonZone;
        Moon.PlayerEnterMoonZoneYellow -= EnteredMoonZone;
        Moon.PlayerEnterMoonZoneGreen -= EnteredMoonZone;
        Moon.PlayerExitMoonZone -= ExitMoonZone;
        Portal.PlayerEnteredPortal -= EnteredPortal;
    }

    private void Update()
    {
        if (inCombat)
        {
            Debug.Log("PLay Combat Music");
        }
        else
        {
            Debug.Log("Play Free Roam Music");
        }
    }

    void DiscoverMoonAudio()
    {
        Debug.Log("Moon Discovered Sound plays");
    }

    void EnteredMoonZone()
    {
        inCombat = true;
        Debug.Log("Player entered zone, play moon battle music");
    }

    void ExitMoonZone()
    {
        inCombat = false;
        Debug.Log("Player left zone, transition back to free roam music");
    }

    void EnteredPortal()
    {
        Debug.Log("Transition to Win Screen and play winscreen audio");
    }
}
