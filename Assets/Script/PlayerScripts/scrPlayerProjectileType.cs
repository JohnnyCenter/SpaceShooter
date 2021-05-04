using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerProjectileType : MonoBehaviour
{
    [Header("Decide the damage types")]
    [SerializeField] private bool IsBallistic;
    [SerializeField] private bool IsEnergy;
    [SerializeField] private bool IsPlasma;
    public int DamageTypeIndex { get; private set; }
    private void Start()
    {
        if(IsBallistic)
        {
            DamageTypeIndex = 1;
        }
        if(IsEnergy)
        {
            DamageTypeIndex = 2;
        }
        if(IsPlasma)
        {
            DamageTypeIndex = 3;
        }
    }
}
