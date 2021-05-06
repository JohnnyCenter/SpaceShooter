using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
   
  
    private AudioSource freeroam;
    private GameObject freeroamaudio;
    public AudioSource MoonEntered;
    public AudioSource MoonDiscovered;
    public AudioSource PortalEntered;
    public AudioSource Sabotage, Scrapper, Scavenger, Stronghold;


    private void Start()
    {
        freeroamaudio = GameObject.FindGameObjectWithTag("FreeRoamTrack");
        freeroam = freeroamaudio.gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Moon.MoonCompleted += DiscoverMoonAudio;
        Moon.PlayerEnterMoonZoneRed += EnteredMoonZone;
        Moon.PlayerEnterMoonZoneYellow += EnteredMoonZone;
        Moon.PlayerEnterMoonZoneGreen += EnteredMoonZone;
        Moon.PlayerExitMoonZone += ExitMoonZone;
        Portal.PlayerEnteredPortal += EnteredPortal;
        PowerUpUIManager.SabotageSFX += PlaySabotage;
        PowerUpUIManager.ScavengerSFX += PlayScavenger;
        PowerUpUIManager.ScrapperSFX += PlayScrapper;
        PowerUpUIManager.StrongholdSFX += PlayStronghold;
    }

    private void OnDisable()
    {
        Moon.MoonCompleted -= DiscoverMoonAudio; 
        Moon.PlayerEnterMoonZoneRed -= EnteredMoonZone;
        Moon.PlayerEnterMoonZoneYellow -= EnteredMoonZone;
        Moon.PlayerEnterMoonZoneGreen -= EnteredMoonZone;
        Moon.PlayerExitMoonZone -= ExitMoonZone;
        Portal.PlayerEnteredPortal -= EnteredPortal;
        PowerUpUIManager.SabotageSFX -= PlaySabotage;
        PowerUpUIManager.ScavengerSFX -= PlayScavenger;
        PowerUpUIManager.ScrapperSFX -= PlayScrapper;
        PowerUpUIManager.StrongholdSFX -= PlayStronghold;
    }

    private void Update()
    {
      /*  if (Enteredmoon == true)
        {
            Debug.Log("PLay Combat Music");
            MoonEntered.Play();
           // MoonDiscovered.Stop();
            freeroam.Stop();

        }
        else
        {
           // Debug.Log("Play Free Roam Music");
            // freeroam.Play();

        }

        if (moonDiscovered == true)
        {
            
            MoonDiscovered.Play();
            Debug.Log("PlayMoonDiscovered");

        }
        else
        {
          //   freeroam.Play();
        }

        if (ExitMoonzone == true)
        {
            freeroam.Play();
            Debug.Log("Playfreeroam");
        }
       
*/
        
    }

    void DiscoverMoonAudio()
    {
        Debug.Log("Moon Discovered Sound plays");
       
        MoonDiscovered.Play();

    }

    void EnteredMoonZone()
    {
        freeroam.Pause();
        MoonEntered.Play();
       
        Debug.Log("Player entered zone, play moon battle music");
    }

    void ExitMoonZone()
    {
        freeroam.UnPause();
        MoonEntered.Stop();
        
        Debug.Log("Player left zone, transition back to free roam music");
    }

    void EnteredPortal()
    {
        PortalEntered.Play();
        Debug.Log("Transition to Win Screen and play winscreen audio");
    }

    void PlaySabotage()
    {
        Sabotage.Play();
        Debug.Log("AUDIOMANAGER: Sabotage sound plays");
    }

    void PlayScrapper()
    {
        Scrapper.Play();
    }

    void PlayScavenger()
    {
        Scavenger.Play();
    }

    void PlayStronghold()
    {
        Stronghold.Play();
    }
}
