using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingColor : MonoBehaviour
{
    Image image;
    public bool flashing;

    private void Awake()
    {
        image = GetComponent<Image>();
        flashing = false;
    }

    private void Update()
    {
        if (flashing)
        {
            image.color = Color.Lerp(Color.clear, Color.white, Mathf.PingPong(Time.time, 0.5f));
        }
    }
}
