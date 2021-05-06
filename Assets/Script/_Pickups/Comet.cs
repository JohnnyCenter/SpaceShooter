using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lean.Common;

public class Comet : MonoBehaviour
{
    [SerializeField]
    private int health, cometLifeTime;
    public static event Action ActivatePowerUp, StartingCycle;
    private bool powerActivated, invisible, flyOff, disconnected;
    SpriteRenderer sr;
    CircleCollider2D cc2D;
    LeanConstrainToCollider ctc;
    GameObject boundary;
    private float spawnOffset;
    ParticleSystem Sparkles;
    playerController player;
    private AudioSource CometSound;


    private void Awake()
    {
        health = 10;
        powerActivated = false;
        sr = GetComponent<SpriteRenderer>();
        cc2D = GetComponent<CircleCollider2D>();
        ctc = GetComponent<LeanConstrainToCollider>();
        boundary = GameObject.FindGameObjectWithTag("CometBoundary");
        ctc.Collider = boundary.GetComponent<BoxCollider>();
        AdjustSpawn();
        Sparkles = GetComponentInChildren<ParticleSystem>();
        StartCoroutine("TimeLimit");
        flyOff = false;
        player = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<playerController>();
        CometSound = GetComponent<AudioSource>();
    }

    void AdjustSpawn()
    {
        spawnOffset = UnityEngine.Random.Range(-7, 7);
        transform.position += transform.right * spawnOffset;
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        Debug.Log("Comet was hit for " + _damage);
    }

    private void Update()
    {
        if (health <= 0 && powerActivated == false)
        {
            StartCoroutine("ActivatePower");
        }

        if (flyOff)
        {
            transform.position -= transform.up * 40 * Time.deltaTime;
        }

        if (player.turning && disconnected == false)
        {
            Discharge();
        }
    }

    private void OnBecameVisible()
    {
        invisible = false;
    }

    private void OnBecameInvisible()
    {
        invisible = true;
    }

    IEnumerator ActivatePower()
    {
        powerActivated = true;
        ActivatePowerUp?.Invoke();
        StopCoroutine("TimeLimit");
        sr.enabled = false;
        cc2D.enabled = false;
        Sparkles.Play();
        Handheld.Vibrate();
        CometSound.Stop();
        yield return new WaitForSeconds(30);
        StartingCycle?.Invoke();
        Destroy(gameObject);
    }

    IEnumerator TimeLimit()
    {
        yield return new WaitForSeconds(cometLifeTime);
        ctc.enabled = false;
        flyOff = true;
        CometSound.Stop();
        yield return new WaitUntil(() => invisible == true);
        StartingCycle?.Invoke();
        Destroy(gameObject);
    }

    void Discharge()
    {
        disconnected = true;
        ctc.enabled = false;
        flyOff = true;
    }
}
