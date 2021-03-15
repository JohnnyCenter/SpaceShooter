using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 10; //Variable that defines the speed of the ship

    [SerializeField]
    float movementShootingSpeed = 5;

    Rigidbody2D rb; //Names Rigidbody2D rb

    bool moveAllowed = false; //Bool for if the player is allowed to move left and right

    private void Start()
    {
        rb.GetComponent<Rigidbody2D>(); //Grabs the Rigidbody2D component from GameObject
    }

    private void Update()
    {
        #region Acceleration
        if (moveAllowed) //If the player is moving the ship side to side
        {
            rb.velocity = new Vector2(rb.velocity.x, movementShootingSpeed); //Acceleration when the player is moving side to side
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed); //Normal acceleration
        }
        #endregion

        #region Movement
        if (Input.touchCount > 0) //If player is touching screen
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); //touchPos variable is defined by where the finger is touching

            switch (touch.phase)
            {
                case TouchPhase.Began: //When the GameObject is touched by the player

                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        moveAllowed = true; //The ship can now move left and right
                    }
                    break;

                case TouchPhase.Moved: //While moving the GameObject

                    if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPos) && moveAllowed) 
                        rb.MovePosition(new Vector2(touchPos.x, transform.position.y)); //GameObjects moves towards the last position of the finger
                    break;

                case TouchPhase.Ended: //When releasing finger

                    moveAllowed = false; //GameObject no longer moves left and right.
                    break;
            }

        }
        #endregion
    }
    public void adjustSpeed(float newSpeed) //Function to adjust the moveSpeed of the player.
    {
        moveSpeed = newSpeed;
    }
}