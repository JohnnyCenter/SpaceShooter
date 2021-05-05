using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrUpgradeMenuCameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject playerBody;
    void Update()
    {
        transform.LookAt(playerBody.transform.position, playerBody.transform.up);
    }
}
