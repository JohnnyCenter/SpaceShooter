using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBeybladeRotation : MonoBehaviour
{
    private scrEnemyStats localStats;

    [SerializeField] private Vector3 beyladeRotation;
    [SerializeField] private float beybladeRotationSpeed;
    [SerializeField] private float rotateTime;
    private float timeSinceStartedRotation;
    public bool RotateFast { get; private set; }

    private void Awake()
    {
        localStats = GetComponentInParent<scrEnemyStats>();
    }
    private void Start()
    {
        RotateFast = false;
    }
    void Update()
    {
        if(timeSinceStartedRotation >= rotateTime)
        {
            RotateFast = false;
        }
        if (RotateFast && timeSinceStartedRotation <= rotateTime)
        {
            timeSinceStartedRotation += Time.deltaTime;
            transform.Rotate((beyladeRotation * beybladeRotationSpeed) * Time.deltaTime);
        }
        if (!RotateFast)
        {
            transform.Rotate((beyladeRotation * beybladeRotationSpeed / 4) * Time.deltaTime);
        }
    }
    public void SetRotate()
    {
        timeSinceStartedRotation = 0f;
        RotateFast = true;
    }
    private void OnEnable()
    {
        localStats.OnEnemyHit += SetRotate;
    }
    private void OnDisable()
    {
        localStats.OnEnemyHit -= SetRotate;
    }
}
