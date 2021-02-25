using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrUpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;

    private void Start()
    {
        upgradePanel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            upgradePanel.SetActive(true);
        }
    }
    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
}
