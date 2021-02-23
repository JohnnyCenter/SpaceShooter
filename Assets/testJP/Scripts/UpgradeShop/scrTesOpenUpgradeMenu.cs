using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTesOpenUpgradeMenu : MonoBehaviour
{
    //Get the reference to the upgrade script through the inspector for now
    [SerializeField] private scrUpgradeManagerUI upgradeManager;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            //Open upgrade pannel
            upgradeManager.OpenUpgradePanel();
        }
    }
}
