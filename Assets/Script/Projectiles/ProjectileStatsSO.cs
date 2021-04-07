using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Projectile/Stats"))]
public class ProjectileStatsSO : ScriptableObject
{
    [Header("Assign stats for projectile")]
    [SerializeField] private int weaponDamage;
    public int WeaponDamage { get; private set; }
    [Tooltip("A higher value means more time between each shot")]
    [SerializeField] private float weaponFireRate;
    public float WeaponFireRate { get; private set; }

    public void ResetStats()
    {
        WeaponDamage = weaponDamage;
        WeaponFireRate = weaponFireRate;
    }
}
