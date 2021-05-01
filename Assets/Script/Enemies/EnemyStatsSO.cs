using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Stats")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("Stats")]
    [Tooltip("How much health the enemy has")]
    [SerializeField] private int health;
    [Tooltip("How much scrap you gain from killing it")]
    [SerializeField] private int scrapReward;
    [Tooltip("How much damage player takes on collision with enemy. Only aplies to the suicide bomber")]
    [SerializeField] private int damageDealthOnCollision;
    public int Health { get; private set; }
    public int ScrapReward { get; private set; }
    public int DamageDealthOnCollision { get; private set; }

    public void resetStats()
    {
        Health = health;
        ScrapReward = scrapReward;
        DamageDealthOnCollision = damageDealthOnCollision;
    }
}
