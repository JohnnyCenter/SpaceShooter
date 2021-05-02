using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using System;

public class playerController : MonoBehaviour
{
    public static Action<Quaternion> OnPlayerTurning; //JONT ADDITION. Used to make enemies turn with the player
    public float moveSpeed = 10; //Variable that defines the speed of the ship
    public bool firing = false;
    public bool turning = false;
    LeanSelectable ls;
    LeanMultiUpdate lms;
    public GameObject Compass;
    LeanSelectable compassActive;
    tempCompass rotation;
    public AudioSource Shipvolume1;
    public AudioSource Shipvolume2;
    public AudioSource Shipvolume3;
    public bool shipvolume1, shipvolume2, shipvolume3;
    

    private void Awake()
    {
        ls = GetComponent<LeanSelectable>();
        compassActive = Compass.GetComponent<LeanSelectable>();
        rotation = GetComponent<tempCompass>();
        lms = GetComponent<LeanMultiUpdate>();
        Shipvolume1.Play();
        shipvolume1 = true;
        shipvolume2 = false;
        shipvolume3 = false;
    }

   

    private void Update()
    {
        #region Acceleration
        if (firing == true && turning == false)
        {
            transform.position += transform.up * (moveSpeed / 2) * Time.deltaTime;
        }
        else if (turning == false && firing == false)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        #endregion
        if (turning)
        {
            rotation.enabled = true;
            lms.enabled = false;
        }
        else
        {
            rotation.enabled = false;
            OnPlayerTurning?.Invoke(transform.rotation); //Added by JONT
            lms.enabled = true;
        }

        if (ls.IsSelected)
        {
            firing = true;
        }
        else
        {
            firing = false;
        }

        if (compassActive.IsSelected)
        {
            turning = true;
        }


        if (moveSpeed <= 15 && moveSpeed > 10 && shipvolume1 == false)
        {
            PlayEngine1();

           
        }

        if(moveSpeed <= 25 && moveSpeed > 15 && shipvolume2 == false)
        {
            PlayEngine2();
        }
 
        if (moveSpeed <= 30 && moveSpeed > 25 && shipvolume3 == false)
        {
            PlayEngine3();
        }
      
    }
    public void adjustSpeed(float newSpeed) //Function to adjust the moveSpeed of the player.
    {
        moveSpeed = newSpeed;
    }

    #region Sound

    void PlayEngine1()
    {
        Debug.Log("Sound should play");
        Shipvolume1.Play();

        Shipvolume2.Stop();
        Shipvolume3.Stop();
        shipvolume1 = true;
        shipvolume2 = false;
        shipvolume3 = false;
    }
    void PlayEngine2()
    {
        Debug.Log("15-25");
        Shipvolume2.Play();
        Shipvolume1.Stop();
        Shipvolume3.Stop();

        shipvolume2 = true;
        shipvolume1 = false;
        shipvolume3 = false;
    }

        void PlayEngine3()
        {       
            Debug.Log("25-30");
            Shipvolume3.Play();
            Shipvolume1.Stop();
            Shipvolume2.Stop();

            shipvolume3 = true;
            shipvolume2 = false;
            shipvolume1 = false;
        }
    #endregion
}