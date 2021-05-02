using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class cameraFollow : MonoBehaviour
{
    public playerController thePlayer;
    public Camera cam;
    [SerializeField]
    GameObject compass;
    LeanSelectable ls;
    UnityEvent deselect;
    private bool playerDead;

    [SerializeField]
    private int rotationSpeed; 
    private Vector3 playerPosition;
    [SerializeField]
    private int currentCamRotation, playerRotation, rotateDirection;
    private bool rotating, rotatingLeft, rotatingRight; 

    private void OnEnable()
    {
        playerBounds.onPlayerEnterBounds += camRelocate;
        scrPlayerHealth.OnPlayerDeath += KillCamera;
    }
    private void OnDisable()
    {
        playerBounds.onPlayerEnterBounds -= camRelocate;
        scrPlayerHealth.OnPlayerDeath -= KillCamera;
    }

    private void Start()
    {
        thePlayer = FindObjectOfType<playerController>();
        ls = compass.GetComponent<LeanSelectable>();
        deselect = ls.OnDeselect;
        deselect.AddListener(beginRotation);
        rotationSpeed = 50;
        playerDead = false;
    }

    private void Update()
    {
        if (!playerDead)
        {
            if (thePlayer.firing == true && thePlayer.turning == false)
            {
                transform.position += transform.up * (thePlayer.moveSpeed / 2) * Time.deltaTime;
            }
            else if (thePlayer.firing == false && thePlayer.turning == false)
            {
                transform.position += transform.up * thePlayer.moveSpeed * Time.deltaTime;
            }
        }

        currentCamRotation = Mathf.RoundToInt(transform.eulerAngles.z);

        if (rotating)
        {
            if (currentCamRotation != playerRotation)
            {
                if ((rotateDirection < 180 && rotateDirection >= 0) || (rotateDirection <= -360 && rotateDirection >= -180))
                {
                    transform.RotateAround(playerPosition, Vector3.back, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.RotateAround(playerPosition, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
            } 
        }
    }

    void beginRotation()
    {
        StartCoroutine(CamRotate());
    }

    IEnumerator CamRotate()
    {
        compass.SetActive(false);
        thePlayer.turning = true;
        playerPosition = thePlayer.transform.position;
        playerRotation = Mathf.RoundToInt(thePlayer.transform.eulerAngles.z);
        rotateDirection = currentCamRotation - playerRotation;
        rotating = true;
        yield return new WaitUntil(() => currentCamRotation == playerRotation);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.RoundToInt(playerRotation)); //Resets rotatation for safety purposes
        rotating = false;
        thePlayer.turning = false;
        compass.SetActive(true);
    }

    void camRelocate()
    {
        transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, transform.position.z);
        transform.position += -transform.up * 4; 
        Debug.Log("Cam has been relocated");
    }

    void KillCamera()
    {
        playerDead = true;
        Debug.Log("Stop Camera");
    }
}
