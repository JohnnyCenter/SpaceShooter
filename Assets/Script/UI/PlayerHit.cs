using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHit : MonoBehaviour
{
    FlashingColor blood;
    [SerializeField]
    private float damageEffectTimer;

    private void Awake()
    {
        blood = GetComponent<FlashingColor>();
    }

    private void OnEnable()
    {
        scrPlayerHealth.playerTookDamage += RunDamage;
    }

    private void OnDisable()
    {
        scrPlayerHealth.playerTookDamage -= RunDamage;
    }

    void RunDamage()
    {
        StartCoroutine("Damage");
        Debug.Log("Beginning Effect");
    }

    IEnumerator Damage()
    {
        blood.flashing = true;
        Handheld.Vibrate();
        yield return new WaitForSeconds(damageEffectTimer);
        blood.flashing = false;
        blood.MakeClear();
    }
}
