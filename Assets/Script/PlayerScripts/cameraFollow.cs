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
    private Vector3 currentPlayerPosition;
    LeanSelectable ls;
    UnityEvent deselect;

    private void Start()
    {
        thePlayer = FindObjectOfType<playerController>();
        ls = compass.GetComponent<LeanSelectable>();
        deselect = ls.OnDeselect;
        deselect.AddListener(beginRotation);
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

        rotationZ = thePlayer.transform.eulerAngles.z;

        if (rotationZ != transform.eulerAngles.z && thePlayer.turning == false)
        {
            thePlayer.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    void beginRotation()
    {
        StartCoroutine(CamRotate());
    }

    IEnumerator CamRotate()
    {
        compass.SetActive(false);
        rotationZ = thePlayer.transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);
        currentPlayerPosition = thePlayer.transform.position;
        transform.position = new Vector3(currentPlayerPosition.x, currentPlayerPosition.y, transform.position.z);
        //transform.position += -transform.up * 5;
        compass.SetActive(true);
        thePlayer.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ / 2);
        yield return new WaitForSeconds(1);
        thePlayer.turning = false;
    }
}
