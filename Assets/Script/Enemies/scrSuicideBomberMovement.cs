using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSuicideBomberMovement : MonoBehaviour
{
    private GameObject thePlayer;
    private playerController playerController;
    private scrEnemyStats healthAndStats;

    private float movementSpeed;
    private float playerMovementSpeed;
    private bool canMove;
    private int movementType;
    private float timeSinceLastSwitch;

    private bool MovingTowardsPlayer;
    private void Awake()
    {
        healthAndStats = GetComponent<scrEnemyStats>(); //Get the instance
        thePlayer = GameObject.FindGameObjectWithTag("PlayerBody");
        playerController = thePlayer.GetComponent<playerController>();
    }
    private void Start()
    {
        canMove = false;
        MovingTowardsPlayer = true;
    }
    private void Update()
    {
        playerMovementSpeed = playerController.moveSpeed;
        //print("Player Movement Speed is: " + playerMovementSpeed);
        movementSpeed = ((100f - playerMovementSpeed) * Time.deltaTime) / 20;
        //print("Movement speed is: " + movementSpeed);
        if(canMove)
        {
            if(MovingTowardsPlayer)
            {
                HomingMovement();
            }
            else if(!MovingTowardsPlayer)
            {
                DriftingMovement();
            }
        }
        if(!MovingTowardsPlayer)
        {
            timeSinceLastSwitch += Time.deltaTime;
            if(timeSinceLastSwitch > 2f)
            {
                timeSinceLastSwitch = 0f;
                SwitchMovement(2); //Return to homing
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBody"))
        {
            //print("Hit the player");

            //Deal damage
            scrPlayerHealth playerHealth = thePlayer.GetComponent<scrPlayerHealth>();
            playerHealth.DealDamageToPlayer(healthAndStats.LocalStats.DamageDealthOnCollision);
            //Destroy gameobject
            healthAndStats.EnemyDiesNoReward(); //Kills the enemy, does not give the player a reward
        }
    }
    private void SwitchMovement(int _movementType)
    {
        movementType = _movementType;
        switch(movementType)
        {
            case 1:
                MovingTowardsPlayer = false;
                return;
            case 2:
                MovingTowardsPlayer = true;
                return;
        }
    }
    private void DriftingMovement()
    {
        if(playerController.firing)
        {
            transform.Translate(Vector2.up * ((playerController.moveSpeed / 2) * 2f) * Time.deltaTime);
        }
        if(!playerController.firing)
        {
            transform.Translate(Vector2.up * (playerController.moveSpeed * 2f) * Time.deltaTime);
        }
    }
    private void HomingMovement()
    {
        //print("Moving");
        transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, movementSpeed);
    }
    private void GotHit()
    {
        SwitchMovement(1); //Stop homing
    }
    private void OnBecameVisible()
    {
        canMove = true;
        MovingTowardsPlayer = true;
    }
    private void OnEnable()
    {
        canMove = false;
        MovingTowardsPlayer = true;
        healthAndStats.OnEnemyHit += GotHit;
    }
    private void OnDisable()
    {
        healthAndStats.OnEnemyHit -= GotHit;
    }
}
