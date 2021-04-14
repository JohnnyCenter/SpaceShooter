using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerBounds : MonoBehaviour
{
    Vector2 rayDirection;
    Vector2 rayPosition;
    int rayOffset = 10;
    int spawnOffset = 20;
    Vector2 distance;
    [SerializeField]
    bool North, South, East, West;
    public static event Action onPlayerEnterBounds;

    private void Awake()
    {
        if (North)
        {
            rayPosition = new Vector2(transform.position.x, transform.position.y - rayOffset);
            rayDirection = Vector2.down;

        }
        else if (South)
        {
            rayPosition = new Vector2(transform.position.x, transform.position.y + rayOffset);
            rayDirection = Vector2.up;
        }
        else if (East)
        {
            rayPosition = new Vector2(transform.position.x - rayOffset, transform.position.y);
            rayDirection = Vector2.left;
        }
        else if (West)
        {
            rayPosition = new Vector2(transform.position.x + rayOffset, transform.position.y);
            rayDirection = Vector2.right;
        }
    }

    private void Start()
    {
        getDistance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            Debug.Log("Made Impact");

            if (North)
            {
                other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y - (distance.y - spawnOffset), other.gameObject.transform.position.z);
                Debug.Log("MovedDownSouth");
                onPlayerEnterBounds?.Invoke();
            }
            else if (South)
            {
                other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y - (distance.y + spawnOffset), other.gameObject.transform.position.z);
                Debug.Log("MovedUpNorth");
                onPlayerEnterBounds?.Invoke();
            }
            else if (East)
            {
                other.gameObject.transform.position = new Vector3(transform.position.x - (distance.x - spawnOffset), other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                Debug.Log("MovedToWest");
                onPlayerEnterBounds?.Invoke();
            }
            else if (West)
            {
                other.gameObject.transform.position = new Vector3(transform.position.x - (distance.x + spawnOffset), other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                Debug.Log("MovedToEast");
                onPlayerEnterBounds?.Invoke();
            }

        }
    }

    void getDistance()
    {
        int layer_mask = LayerMask.GetMask("PlayerBounds");

        RaycastHit2D hit = Physics2D.Raycast(rayPosition, rayDirection, Mathf.Infinity, layer_mask);

        if (hit.collider != null)
        {
            Debug.Log(hit.transform.name);
            distance = new Vector2(transform.position.x - hit.point.x, transform.position.y - hit.point.y);
            Debug.Log(distance);
        }
        else
        {
            Debug.Log("Error: Couldn't find bounds");
        }
    }
}
