using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PowerUpUIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] PowerUpText;
    [SerializeField]
    GameObject[] PowerUpImage;
    [SerializeField]
    AudioClip[] PowerUpSound;
    private int powerType, powerUpTime;

    private void Awake()
    {
        foreach (TextMeshProUGUI text in PowerUpText)
        {
            text.enabled = false;
        }

        foreach(GameObject image in PowerUpImage)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        scrSabotage.OnSabotageTriggered += Sabotage;
    }

    private void OnDisable()
    {
        scrSabotage.OnSabotageTriggered -= Sabotage;
    }

    void Sabotage()
    {
        powerType = 0;
        powerUpTime = 4;
        PowerUpImage[0].SetActive(true);
        PowerUpText[0].enabled = true;
        StartCoroutine("Sequence");
        Debug.Log("POWER-UP: Sabotage Activated");
    }

    IEnumerator Sequence()
    {
        Debug.Log("POWER-UP: Animation begun");
        PowerUpText[powerType].rectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f);
        var image = PowerUpImage[powerType].GetComponent<Image>();
        image.DOFade(255, 1f);
        yield return new WaitForSeconds(0.5f);
        PowerUpText[powerType].rectTransform.DOAnchorPos(new Vector2(-650, 0), 0.5f);
        yield return new WaitForSeconds(powerUpTime);
        var flashEffect = PowerUpImage[powerType].GetComponent<FlashingColor>();
        if(flashEffect != null)
        {
            flashEffect.flashing = true;
        }
        yield return new WaitForSeconds(3f);
        flashEffect.flashing = false;
        PowerUpText[powerType].rectTransform.anchoredPosition = new Vector2(650, 0);
        image.color = Color.clear;
    }
}
