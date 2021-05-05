using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalMiniMap : MonoBehaviour
{
    SpriteRenderer mapIcon;

    private void Awake()
    {
        mapIcon = GetComponent<SpriteRenderer>();
        mapIcon.enabled = false;
    }

    private void OnEnable()
    {
        Portal.PortalOpen += EnableIcon;
    }

    private void OnDisable()
    {
        Portal.PortalOpen -= EnableIcon;
    }

    void EnableIcon()
    {
        mapIcon.enabled = true;
        Debug.Log("Portal is now on map");
    }
}
