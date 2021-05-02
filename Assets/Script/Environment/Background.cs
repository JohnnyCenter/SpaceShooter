using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Background : MonoBehaviour
{
    [SerializeField]
    float parallaxEffectMultiplier = 0.5f; [Tooltip("How fast is the background going to scroll? (The closer to 1 the slower it moves)")]
    private Transform camTransform;
    private Vector3 lastCameraPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    SpriteRenderer sr;
    [SerializeField]
    private float colorFadeSpeed, colorFadeTime; [Tooltip("How fast should the background change color when entering a moon zone?")]
    [SerializeField]
    private float playerSpeed, speedTreshold;
    public playerController player;

    #region Parallax Effect
    private void Awake()
    {
        camTransform = Camera.main.transform;
        lastCameraPos = camTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - lastCameraPos;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPos = camTransform.position;

        if (Mathf.Abs(camTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (camTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camTransform.position.x + offsetPositionX, transform.position.y);  
        }

        if (Mathf.Abs(camTransform.position.y - transform.position.y) >= textureUnitSizeY)
        {
            float offsetPositionY = (camTransform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, camTransform.position.y + offsetPositionY);
        }
    }
    #endregion

    #region Background Color Change
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<playerController>();
        speedTreshold = 15; //Defaults the speedTreshold to 15
    }

    private void OnEnable()
    {
        Moon.PlayerEnterMoonZoneRed += ShiftToRed;
        Moon.PlayerEnterMoonZoneYellow += ShiftToYellow;
        Moon.PlayerEnterMoonZoneGreen += ShiftToGreen;
        Moon.PlayerExitMoonZone += ShiftColorBack;
    }

    private void OnDisable()
    {
        Moon.PlayerEnterMoonZoneRed -= ShiftToRed;
        Moon.PlayerEnterMoonZoneYellow -= ShiftToYellow;
        Moon.PlayerEnterMoonZoneGreen -= ShiftToGreen;
        Moon.PlayerExitMoonZone -= ShiftColorBack;
    }

    private void Update()
    {
        playerSpeed = player.moveSpeed; //Links playerSpeed to moveSpeed from PlayerController

        if (playerSpeed < speedTreshold) //If the player moves slow, slow down the fading of the color
        {
            colorFadeSpeed = colorFadeTime;
        }
        else if (playerSpeed >= speedTreshold) //If the player is the same or over the treshold, speed up the fading of color
        {
            colorFadeSpeed = colorFadeTime / 2;
        }
    }

    void ShiftToRed() //Changes color to red over time
    {
        sr.DOColor(Color.red, colorFadeSpeed);
    }

    void ShiftToYellow() //Changes color to Yellow over time
    {
        sr.DOColor(Color.yellow, colorFadeSpeed);
    }

    void ShiftToGreen() //Changes color to Green over time
    {
        sr.DOColor(Color.green, colorFadeSpeed);
    }

    void ShiftColorBack() //Changes color back to normal over time
    {
        sr.DOColor(Color.white, colorFadeSpeed);
    }
    #endregion
}
