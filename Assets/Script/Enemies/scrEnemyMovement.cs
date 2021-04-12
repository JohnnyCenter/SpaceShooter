using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class scrEnemyMovement : MonoBehaviour
{
    private GameObject thePlayer;

    private bool iAmActive;
    public bool IAmActive { get; private set; }
    private playerController playerController;
    [SerializeField] private float sidewaysMovementSpeed;
    private bool moveRight;
    private float switchSideCounter;
    private float switchSideValue;
    private bool canMove;
    private float movementSpeed;

    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("PlayerBody");
        playerController = thePlayer.GetComponent<playerController>();
    }

    private void Start()
    {
        iAmActive = false;
        IAmActive = iAmActive;
        switchSideCounter = 0f;
        switchSideValue = GetRandomNumber();
        canMove = false;
    }
    private void Update()
    {
        movementSpeed = playerController.moveSpeed; //Updates the movement speed with that of the player
        if (IAmActive)
        {
            BasicEnemyMovement();
        }
    }
    private void BasicEnemyMovement()
    {
        if(canMove)
        {
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
        }
        switchSideCounter += Time.deltaTime;
        if(switchSideCounter >= switchSideValue)
        {
            moveRight = !moveRight;
            switchSideCounter = 0f;
            switchSideValue = GetRandomNumber();
        }
        if(moveRight)
        {
            transform.Translate(Vector2.right * sidewaysMovementSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.right * sidewaysMovementSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyCollider"))
        {
            canMove = true;
        }
    }
    private float GetRandomNumber()
    {
        return Random.Range(0.5f, 2f);
    }

    //2. Set player as target

    //3. Move towards the player

    //4. Fire projectiles that move in a straight direction
    //1. Detect the player (onBecameVisible?)
    private void OnBecameVisible()
    {
        //print("Hello player!");
        IAmActive = true;
    }
    private void OnBecameInvisible()
    {
        canMove = false;
        IAmActive = false;
    }
}
