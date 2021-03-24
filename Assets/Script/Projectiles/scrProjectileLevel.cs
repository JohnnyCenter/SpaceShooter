using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProjectileLevel : MonoBehaviour
{
    public void UpdateProjectileLevel(int _assignedLevel)
    {
        switch(_assignedLevel) //Change what STATS(SO) is used based on level
        {
            case 0:
                //Use specified SO for stats
                return;
            case 1:
                return;
            case 2:
                return;
            case 3:
                return;
            case 4:
                return;
            default:
                Debug.LogError("The value for the int passed to the UpdateProjectileLevel funcion in the scrProjectileLevel class is invalid.");
                return;
        }
    }
}
