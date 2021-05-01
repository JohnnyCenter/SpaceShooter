using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenAudio : MonoBehaviour
{
    public AudioSource wingame;
    private AudioSource[] allAudioSources;

    public void Awake()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Pause();
        }

        playwin();


    }

    void playwin()
    {
        wingame.Play();
    }




}
