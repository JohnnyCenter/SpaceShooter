using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    GameObject TouchManager;
    public static event Action StartEngine;
    private cameraFollow cam;
    scrCircleSpawner spawner; //Temporary for Circle Spawner
    CometSpawner cometSpawner;
    
    private void Awake()
    {
        TouchManager.gameObject.SetActive(false);
        cam = FindObjectOfType<cameraFollow>();
        cam.enabled = false;
        spawner = FindObjectOfType<scrCircleSpawner>(); //Temp needs to be deleted
        spawner.enabled = false; //Temp needs to be deleted
        cometSpawner = FindObjectOfType<CometSpawner>();
        cometSpawner.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startEngine();
        }
    }

    public void startEngine()
    {
        StartEngine?.Invoke();
        Handheld.Vibrate();
        TouchManager.gameObject.SetActive(true);
        cam.enabled = true;
        var text = gameObject.transform.Find("Tap to Start");
        text.gameObject.SetActive(false);
        var image = gameObject.transform.Find("Button");
        image.gameObject.SetActive(false);
        spawner.enabled = true; // Temporary
        cometSpawner.enabled = true;
        Debug.Log("Game Starting");
    }
}
