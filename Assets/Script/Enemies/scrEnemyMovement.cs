using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class scrEnemyMovement : MonoBehaviour
{
    private GameObject thePlayer;
    private scrEnemyStats localStats;

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
    public bool IsHit { get; private set; }
    private float spinTimer;

    //Sabotage related
    private bool sabotaged;
    private float timeSinceSabotaged;
    [Tooltip("How many seconds the enemy is sabotaged")]
    [SerializeField] private float sabotageTime = 10f;

    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectWithTag("PlayerBody");
        playerController = thePlayer.GetComponent<playerController>();
        localStats = GetComponent<scrEnemyStats>();
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
        spinTimer = 0f;
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
        if(sabotaged)
        {
            timeSinceSabotaged += Time.deltaTime;
            if(timeSinceSabotaged >= sabotageTime)
            {
                timeSinceSabotaged = 0;
                sabotaged = false;
            }
        }
    }
    private void BasicEnemyMovement()
    {
        if (canMove)
        {
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
        }
        switchSideCounter += Time.deltaTime;
        if (switchSideCounter >= switchSideValue)
        {
            moveRight = !moveRight;
            switchSideCounter = 0f;
            switchSideValue = GetRandomNumber();
        }
        if (moveRight && !collidingWithRightBorder && !sabotaged)
        {
            transform.Translate(Vector2.right * sidewaysMovementSpeed * Time.deltaTime);
        }
        else if (!collidingWithLeftBorder && !sabotaged)
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
    private void SetIsHit()
    {
        IsHit = true;
        //spinTimer = 0f;
    }
    private void IsSabotaged()
    {
        sabotaged = true;
        timeSinceSabotaged = 0f;
    }
    private void OnEnable()
    {
        scrSabotage.OnSabotageTriggered += IsSabotaged;
        playerController.OnPlayerTurning += RotateEnemy;
        localStats.OnEnemyHit += SetIsHit;
    }
    private void OnDisable()
    {
        scrSabotage.OnSabotageTriggered -= IsSabotaged;
        playerController.OnPlayerTurning -= RotateEnemy;
        localStats.OnEnemyHit -= SetIsHit;
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
