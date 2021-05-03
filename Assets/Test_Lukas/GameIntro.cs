using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    public AudioSource introgame;
    public AudioSource menu;
  

    private void Start()
    {
        introgame.Play();
    }
    public void OpenMenu()
    {
        introgame.Stop();
        menu.Play();
       
    }

   
}
