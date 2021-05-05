using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] allAudioSources;
    public AudioSource inGameMenu;
    public bool ispaused = false;
    public AudioSource intro;
    private AudioSource StartFreeroam;
    private GameObject TheSource;
    private void Awake()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        

    }

    private void Start()
    {
        intro.Play();
        TheSource = GameObject.FindGameObjectWithTag("FreeRoamTrack");
        StartFreeroam = TheSource.gameObject.GetComponent<AudioSource>();
    }
    public void stopallaudio()
   {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Pause();
        }

        if (ispaused == false)
        {
             play();

        }

       if (ispaused == true)
       {

            inGameMenu.UnPause();
       }

   }

    public void startAudio()
    {
        foreach(AudioSource audioS in allAudioSources)
        {

         audioS.UnPause();
        }

        inGameMenu.Pause();
    }

    void play()
    {
        inGameMenu.Play();
        ispaused = true;
    }

    public void startfreeroack()
    {
        StartFreeroam.Play();
        intro.Stop();
    }

}
