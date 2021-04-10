using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class scrEnemyMovement : MonoBehaviour
{
    private bool iAmActive;
    public bool IAmActive { get; private set; }
    Lean.Common.LeanConstrainToCollider LeanConstrainToCollider;
    [SerializeField] private float movementSpeed;
    private bool moveRight;
    private float switchSideCounter;
    private float switchSideValue;

    private void Awake()
    {
        LeanConstrainToCollider = GetComponent<Lean.Common.LeanConstrainToCollider>();
    }

    private void Start()
    {
        iAmActive = false;
        IAmActive = iAmActive;
        LeanConstrainToCollider.enabled = false;
        switchSideCounter = 0f;
        switchSideValue = GetRandomNumber();
    }
    private void Update()
    {
        if(IAmActive)
        {
            BasicEnemyMovement();
        }
    }
    private void BasicEnemyMovement()
    {
        switchSideCounter += Time.deltaTime;
        if(switchSideCounter >= switchSideValue)
        {
            moveRight = !moveRight;
            switchSideCounter = 0f;
            switchSideValue = GetRandomNumber();
        }
        if(moveRight)
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.right * movementSpeed * Time.deltaTime);
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
        iAmActive = true;
        IAmActive = iAmActive;
        LeanConstrainToCollider.enabled = true;
    }
    private void OnBecameInvisible()
    {
        iAmActive = false;
        IAmActive = iAmActive;
        LeanConstrainToCollider.enabled = false;
    }
}
