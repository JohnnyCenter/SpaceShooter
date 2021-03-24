using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Projectile/Stats"))]
public class ProjectileStatsSO : ScriptableObject
{
    [Header("Assign stats for projectile")]
    [SerializeField] private int weaponDamage;
    [SerializeField] private float weaponFireRate;
}
