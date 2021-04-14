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
    public GameObject Compass;
    LeanSelectable compassActive;
    tempCompass rotation;

    private void Awake()
    {
        ls = GetComponent<LeanSelectable>();
        compassActive = Compass.GetComponent<LeanSelectable>();
        rotation = GetComponent<tempCompass>();
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
        }
        else
        {
            rotation.enabled = false;
            OnPlayerTurning?.Invoke(transform.rotation); //Added by JONT
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

    }
    public void adjustSpeed(float newSpeed) //Function to adjust the moveSpeed of the player.
    {
        moveSpeed = newSpeed;
    }
}