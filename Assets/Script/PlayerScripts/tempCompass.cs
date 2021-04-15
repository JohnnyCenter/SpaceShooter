using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempCompass : MonoBehaviour
{
    [SerializeField]
    GameObject compass;
    float rotationZ;

    private void Update()
    {
        rotationZ = compass.transform.localEulerAngles.z;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);
    }
}
