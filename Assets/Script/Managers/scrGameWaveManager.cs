using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGameWaveManager : MonoBehaviour
{
    //Make this a singleton
    [Tooltip("What wave we start at. Tweakable for testing purposes")]
    [SerializeField] private int gameStartWave = 0;
    public int CurrentWave { get; private set; }
    //public static scrGameWaveManager gameWaveManager;
    private void Awake()
    {
        //gameWaveManager = this;
        CurrentWave = gameStartWave; //Do not move this to start!
    }

    public void StartNewWave() //Reference this funciton to start a new wave
    {
        CurrentWave += 1;
    }
    public void IncreaseWaveByNumber(int _number) //Reference this function to increase the wave by set number
    {
        CurrentWave += _number;
    }
}
