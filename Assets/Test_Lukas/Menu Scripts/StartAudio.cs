using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour
{
    public AudioSource intro;
    public AudioSource StartFreeroam;
    // Start is called before the first frame update

    private void Start()
    {
        intro.Play();
        
    }

    public void startfreeroack()
    {
        StartFreeroam.Play();
    }
}
