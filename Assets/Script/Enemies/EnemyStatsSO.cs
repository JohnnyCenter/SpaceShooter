using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Stats")]
public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] private int health;
    public int Health { get; private set; }

    public void resetStats()
    {
        Health = health;
    }
}
