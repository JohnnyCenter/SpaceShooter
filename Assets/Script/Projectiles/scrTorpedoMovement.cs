using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTorpedoMovement : scrProjectileMovement
{
    private List<GameObject> targets;
    private GameObject torpedoTarget;
    private float minDistanceToTarget = 0.1f;
    protected override void Awake()
    {
        base.Awake();
        playerController = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<playerController>(); //Gets the reference
        targets = new List<GameObject>();
        torpedoTarget = null;
    }
    protected override void MovePorjectile()
    {
        if (playerController != null)
        {
            if(playerController.firing)
            {
                playerMovementSpeed = playerController.moveSpeed / 2;
            }
            else if(!playerController.firing)
            {
                playerMovementSpeed = playerController.moveSpeed;
            }
        }
        MoveTorpedo();
    }
    private void MoveTorpedo()
    {
        if(torpedoTarget == null)//No targets? Move the projectile as normal
        {
            //print("moving rocket with default movement method");
            base.MovePorjectile();
        }
        else if(torpedoTarget != null) //Target? Home in on target.
        {
            //print("Projectile homing in on target");
            //RotateTorpedo();
            transform.position = Vector3.MoveTowards(transform.position, torpedoTarget.transform.position, (movementSpeed + playerMovementSpeed) * Time.deltaTime * 50);
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
    public void RemoveTargetFromList(GameObject _target)
    {
        if (_target != null)
        {
            //print("Target added to list");
            targets.Remove(_target);
            torpedoTarget = targets[0];
        }
    }
}
