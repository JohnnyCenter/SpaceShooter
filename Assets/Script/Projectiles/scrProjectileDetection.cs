using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get reference and send it to scrTorpedoMovement
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Loose reference and update scrTorpedoMovement
    }
}
