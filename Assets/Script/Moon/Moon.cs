using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Moon : MonoBehaviour
{
    [SerializeField]
    int moonDistance; [Tooltip("How close does the player have to be for the moon to be discovered?")] //Determines how close the player needs to be to the moon to discover it !!Must be assigned in inspector or default value will be zero!!
    bool discovered; //Has the moon already been discovered?
    [SerializeField]private float distance; //The distance between player and the middle of the moon
    
    [SerializeField]
    [Tooltip("Choose what color the background will change to when the player gets close")]
    bool Red, Green, Yellow; //What color is the moon, chosen in the Inspector
    public static event Action PlayerEnterMoonZoneRed, PlayerEnterMoonZoneYellow, PlayerEnterMoonZoneGreen, PlayerExitMoonZone; //Events regarding the backgrounds
    public static event Action MoonCompleted; //Event that runs if the player has discovered the moon

    private void Start()
    {
        discovered = false;
        distance = (moonDistance * moonDistance) * 2; //Making the distance variable twice the size of the moonDistance so that all the moons don't get discovered at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody")) //If player enters the triggerbox to the moon, change color depending on what bool is active
        {
            Debug.Log("Player has entered zone");
            if (Red)
            {
                PlayerEnterMoonZoneRed?.Invoke();
            }
            else if (Green)
            {
                PlayerEnterMoonZoneGreen?.Invoke();
            }
            else if (Yellow)
            {
                PlayerEnterMoonZoneYellow?.Invoke();
            }
            else
            {
                Debug.LogError("Moon has not been assigned color!"); //If no color has been selected in the inspector, debug error
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            Vector3 offset = other.transform.position - transform.position; //Changes the origin point from the middle of the scene to the middle of the moon
            distance = offset.sqrMagnitude; //The distance between player and moon

            if (distance < moonDistance * moonDistance && discovered == false)
            {
                moonDiscovered(); //Moon becomes discovered
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) //When players leave the moon triggerbox, reset background colors
    {
        if (other.CompareTag("PlayerBody"))
        {
            Debug.Log("Player has left the zone");
            PlayerExitMoonZone?.Invoke();
        }
    }

    void moonDiscovered()
    {
        discovered = true;
        MoonCompleted?.Invoke();
        Debug.Log("Moon Discovered");
    }
}
