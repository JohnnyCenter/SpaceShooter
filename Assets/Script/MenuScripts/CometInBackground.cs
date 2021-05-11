using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometInBackground : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    private int minRange, maxRange, timeBetween, cometAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine("CometTimer");
    }

    IEnumerator CometTimer()
    {
        timeBetween = Random.Range(minRange, maxRange);
        yield return new WaitForSeconds(timeBetween);
        cometAnim = Random.Range(1, 4);
        StartCoroutine("RunCometAnimation");
    }

    IEnumerator RunCometAnimation()
    {
        anim.SetInteger("Travel", cometAnim);
        yield return new WaitForSeconds(3);
        anim.SetInteger("Travel", 0);
        StartCoroutine("CometTimer");
    }
}
