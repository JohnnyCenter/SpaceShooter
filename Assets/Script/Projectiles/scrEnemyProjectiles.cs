using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scrEnemyProjectiles : MonoBehaviour
{
    public static Action OnPlayerDamagedByEnemy;
    [Tooltip("Decides how much time it takes before the projectile despawns")]
    [SerializeField] private float despawnTimer;
    private float timeSinceSpawned;
    [Range(15f, 30f)]
    [SerializeField] private float movementSpeed = 20f;
    protected playerController playerController;
    protected float playerMovementSpeed;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<playerController>(); //Gets the reference
        playerMovementSpeed = 0f;
    }
    private void Start()
    {
        timeSinceSpawned = 0f;
    }
    private void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if(despawnTimer <= timeSinceSpawned)
        {
            DestroyProjectile();
        }
        if (playerController != null)
        {
            playerMovementSpeed = playerController.moveSpeed;
        }
        if(!playerController.firing)
        {
            transform.position -= (transform.up * (movementSpeed - playerMovementSpeed) * Time.deltaTime);
        }
        else if(playerController.firing)
        {
            transform.position -= (transform.up * (movementSpeed - (playerMovementSpeed / 2)) * Time.deltaTime);
        }
    }
    private void DestroyProjectile()
    {
        timeSinceSpawned = 0f;
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody") == true)
        {
            //print("Hit the player");
            //Deal damage to player through event
            OnPlayerDamagedByEnemy?.Invoke();
            //Dissable this object
            DestroyProjectile();
        }
    }
}
