using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Losescreenaudiomanager : MonoBehaviour
{

    public AudioSource losegame;
    private AudioSource[] allAudioSources;

    public void Awake()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Pause();
        }

        playlose();

        
    }

    void playlose()
    {
        losegame.Play();
    }

 
}
