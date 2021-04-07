using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTorpedoMovement : scrProjectileMovement
{
    private List<GameObject> targets;
    private GameObject torpedoTarget;
    private float minDistanceToTarget = 0.5f;
    private void Awake()
    {
        targets = new List<GameObject>();
    }
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
        else if(torpedoTarget != null) //Target? Home in on target.
        {
            //print("Projectile homing in on target");
            RotateTorpedo();
            transform.position = Vector3.MoveTowards(transform.position, torpedoTarget.transform.position, (movementSpeed + playerMovementSpeed) * Time.deltaTime);
            if((transform.position - torpedoTarget.transform.position).magnitude <= minDistanceToTarget)
            {
                //print("Targets cleared");
                targets.Clear();
                torpedoTarget = null;
                //Hit target:
                //Deal damage
                //Dissable the projectile
            }
        }
    }
    private void RotateTorpedo()
    {
        Vector3 vectorToTarget = transform.position - torpedoTarget.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
    }
    public void AddTargetToList(GameObject _target)
    {
        if(_target != null)
        {
            //print("Target added to list");
            targets.Add(_target);
            torpedoTarget = targets[0];
        }
    }
}
