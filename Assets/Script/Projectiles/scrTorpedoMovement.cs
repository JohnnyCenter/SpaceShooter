using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTorpedoMovement : scrProjectileMovement
{
    private GameObject torpedoTarget;
    protected override void MovePorjectile()
    {
        MoveTorpedo();
    }
    private void MoveTorpedo()
    {
        if(torpedoTarget == null)//No targets? Move the projectile as normal
        {
            base.MovePorjectile();
        }
    }
}
