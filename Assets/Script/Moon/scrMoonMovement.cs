using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMoonMovement : MonoBehaviour
{
    private Vector3 MoonRotation;
    [Tooltip("How fast the moon rotatses")]
    [Range(1, 5)]
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        MoonRotation = new Vector3(2, 0, 5);
    }
    private void Update()
    {
        transform.Rotate((MoonRotation * rotationSpeed) * Time.deltaTime);
    }
}
