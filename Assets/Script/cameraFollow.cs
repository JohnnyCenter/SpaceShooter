using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public playerController thePlayer;

    private Vector3 lastPlayerPosition;
    private float distanceToMoveX, distanceToMoveY;

    public Camera cam;

    private void Start()
    {
        thePlayer = FindObjectOfType<playerController>();
        lastPlayerPosition = thePlayer.transform.position;
    }

    private void Update()
    {
        //distanceToMoveX = thePlayer.transform.position.x - lastPlayerPosition.x;
        distanceToMoveY = thePlayer.transform.position.y - lastPlayerPosition.y;

        transform.position = new Vector3(transform.position.x + distanceToMoveX, transform.position.y + distanceToMoveY, transform.position.z);

        lastPlayerPosition = thePlayer.transform.position;
    }
}
