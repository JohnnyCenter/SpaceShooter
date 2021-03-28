using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testParallax : MonoBehaviour
{
    private Transform camTransform;
    private Vector3 lastCameraPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    [SerializeField]
    float parallaxEffectMultiplier = 0.5f;

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
}
