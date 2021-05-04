using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyWeaknessTracker : MonoBehaviour
{
    [Header("Set weakness, IMPORTANT: Only set one weakness type, or use the weakToAll type.")]
    [SerializeField] private bool weakToEnergy;
    [SerializeField] private bool weakToPlasma;
    [SerializeField] private bool weakToBallistics;
    [SerializeField] private bool weakToAll;

    public bool WeakToEnergy { get; private set; }
    public bool WeakToPlasma { get; private set; }
    public bool WeakToBallistics { get; private set; }
    public bool WeakToAll { get; private set; }

    public int WeaknessIndex { get; private set; }

    private void Start()
    {
        WeakToAll = weakToAll;
        WeakToBallistics = weakToBallistics;
        WeakToEnergy = weakToEnergy;
        WeakToPlasma = weakToPlasma;

        if(WeakToAll)
        {
            WeaknessIndex = 4;
        }
        if(WeakToBallistics)
        {
            WeaknessIndex = 1;
        }
        if(WeakToEnergy)
        {
            WeaknessIndex = 2;
        }
        if(WeakToPlasma)
        {
            WeaknessIndex = 3;
        }
    }
}
