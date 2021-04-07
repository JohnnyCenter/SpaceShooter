using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileDetection : MonoBehaviour
{
    scrTorpedoMovement torpedoMovement;

    private void Awake()
    {
        torpedoMovement = GetComponentInParent<scrTorpedoMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            torpedoMovement.AddTargetToList(collision.gameObject);
        }
        //Get reference and send it to scrTorpedoMovement
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

        }
        //Loose reference and update scrTorpedoMovement
    }
}
