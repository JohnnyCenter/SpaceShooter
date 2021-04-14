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
    private float switchSideCounter;
    private float switchSideValue;
    private float movementSpeed;
    private float playerFiringSpeed;
    private float playerMovementSpeed;
    private bool canMove;
    private bool moveRight;
    private bool collidingWithRightBorder;
    private bool collidingWithLeftBorder;

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
        collidingWithLeftBorder = false;
        collidingWithRightBorder = false;
    }
    private void Update()
    {
        #region UpdateMovementSpeedVar
        playerMovementSpeed = playerController.moveSpeed; //Updates the movement speed with that of the player
        movementSpeed = playerMovementSpeed;
        playerFiringSpeed = playerMovementSpeed / 2;

        if(playerController.firing && !playerController.turning)
        {
            movementSpeed = playerFiringSpeed;
        }
        else if(!playerController.firing && !playerController.turning)
        {
            movementSpeed = playerMovementSpeed;
        }
        #endregion
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
        if(moveRight && !collidingWithRightBorder)
        {
            transform.Translate(Vector2.right * sidewaysMovementSpeed * Time.deltaTime);
        }
        else if(!collidingWithLeftBorder)
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
        if(collision.CompareTag("LeftBorder"))
        {
            collidingWithLeftBorder = true;
        }
        if(collision.CompareTag("RightBorder"))
        {
            collidingWithRightBorder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftBorder"))
        {
            collidingWithLeftBorder = false;
        }
        if (collision.CompareTag("RightBorder"))
        {
            collidingWithRightBorder = false;
        }
    }
    private float GetRandomNumber()
    {
        return Random.Range(0.5f, 2f);
    }
    private void RotateEnemy(Quaternion _newRotation)
    {
        //print("Rotating enemies...");
        transform.rotation = _newRotation;
    }
    private void OnEnable()
    {
        playerController.OnPlayerTurning += RotateEnemy;
    }
    private void OnDisable()
    {
        playerController.OnPlayerTurning -= RotateEnemy;
    }
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
