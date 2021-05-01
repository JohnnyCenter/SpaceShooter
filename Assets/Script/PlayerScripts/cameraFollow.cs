using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Touch;

public class cameraFollow : MonoBehaviour
{
    public playerController thePlayer;
    public Camera cam;
    float rotationZ;
    [SerializeField]
    GameObject compass;
    LeanSelectable ls;
    UnityEvent deselect;

    [SerializeField]
    private int rotationSpeed; 
    private Vector3 playerPosition;
    private int currentCamRotation, playerRotation;
    private bool rotating; 

    private void OnEnable()
    {
        playerBounds.onPlayerEnterBounds += camRelocate;
    }
    private void OnDisable()
    {
        playerBounds.onPlayerEnterBounds -= camRelocate;
    }

    private void Start()
    {
        thePlayer = FindObjectOfType<playerController>();
        ls = compass.GetComponent<LeanSelectable>();
        deselect = ls.OnDeselect;
        deselect.AddListener(beginRotation);
        rotationSpeed = 50;
    }

    private void Update()
    {
        if (thePlayer.firing == true && thePlayer.turning == false)
        {
            transform.position += transform.up * (thePlayer.moveSpeed / 2) * Time.deltaTime;
        }
        else if (thePlayer.firing == false && thePlayer.turning == false)
        {
            transform.position += transform.up * thePlayer.moveSpeed * Time.deltaTime;
        }

        currentCamRotation = Mathf.RoundToInt(transform.eulerAngles.z);

        if (rotating)
        {
            if (currentCamRotation != playerRotation)
            {
                if (currentCamRotation < playerRotation)
                {
                    transform.RotateAround(playerPosition, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
                else if (currentCamRotation > playerRotation)
                {
                    transform.RotateAround(playerPosition, Vector3.back, rotationSpeed * Time.deltaTime);
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
}
