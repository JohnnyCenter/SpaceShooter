using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlashingColor : MonoBehaviour
{
    Image image;
    public bool flashing;
    [SerializeField]
    private float flashSpeed;

    private void Awake()
    {
        image = GetComponent<Image>();
        flashing = false;
        image.color = Color.clear;
    }

    private void Update()
    {
        if (flashing)
        {
            image.color = Color.Lerp(Color.clear, Color.white, Mathf.PingPong(Time.time, flashSpeed));
        }
    }

    public void MakeClear()
    {
        image.DOColor(Color.clear, 0.5f);
    }
}
