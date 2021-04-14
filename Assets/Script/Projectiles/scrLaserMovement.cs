using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLaserMovement : scrProjectileMovement
{
    //HOW THIS INHERITANCE WORKS
    //From testing:
    //By default (if this class would be empty, YET inherit from scrProjectileMovement) this class would run the EXACT same code as that in scrProjectileMovement.
    //Hovewer, it seems that if I add a "update", "start" etc, it OVERWRITES that of the parent class by default, so i would need to make the parent version
    //virtual, and the child version overide.

    protected override void MovePorjectile()
    {
        base.MovePorjectile(); //This works, the base does not need to be called
    }


    private void AnimateLaser()
    {
        //Animate the laser
    }
}
