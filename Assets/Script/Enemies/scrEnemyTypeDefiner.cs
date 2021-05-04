using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyTypeDefiner : MonoBehaviour
{
    [Range(1,6)]
    [SerializeField] private int enemyType;
    public int EnemyType { get; private set; }

    private void Awake()
    {
        EnemyType = enemyType;
    }
    private void OnBecameVisible()
    {
        scrUpgradeMenu.Instance.OpenEnemyInfoCardByType(EnemyType);
    }
}
