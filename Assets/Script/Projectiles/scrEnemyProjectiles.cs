using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyProjectiles : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollider") == true)
        {
            //print("Hit the player");
            //Deal damage to player through event

            //Dissable this object
            this.gameObject.SetActive(false);
        }
    }
}
