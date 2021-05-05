using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreenTips : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] Tips;
    private int tipChosen;
    private bool tipIsDisplaying;

    private void Awake()
    {
       foreach (TextMeshProUGUI tip in Tips)
        {
            tip.enabled = false;
        } 

        ChooseTip();
        tipIsDisplaying = false;
    }

    void ChooseTip()
    {
        tipChosen = Random.Range(0, 16);
        Debug.Log("Tip has been chosen, it's " + tipChosen);
    }

    private void Update()
    {
        if (!tipIsDisplaying)
        {
            DisplayTip();
            Debug.Log("Displaying tip");
        }
    }

    void DisplayTip()
    {
        Tips[tipChosen].enabled = true;
        tipIsDisplaying = true;
        Debug.Log("Tip has been displayed");
    }
}
