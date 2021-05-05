using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMiniMap : MonoBehaviour
{
    [SerializeField] private GameObject miniMap;
    public void CloseMiniMap()
    {
        miniMap.SetActive(false);
        Time.timeScale = 1f;
    }
}
