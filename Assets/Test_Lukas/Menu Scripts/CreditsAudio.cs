using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAudio : MonoBehaviour
{
    public AudioSource creditsgame;
    private AudioSource[] allAudioSources;

    public void Awake()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();


    }

        
    public void playcredit()
    {
        creditsgame.Play();
        foreach (AudioSource audioS in allAudioSources)
        {

            audioS.Pause();
        }
    }

    public void stopPlaying()
    {
        creditsgame.Stop();

        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.UnPause();
        }

    }


}


